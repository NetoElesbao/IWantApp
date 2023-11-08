


using Flunt.Validations;
using IWantApp.Models.Products.Base;

namespace IWantApp.Models.Order
{
    public class Order : Entity
    {
        public Guid ClientId { get; private set; }
        public List<Product> Products { get; private set; }
        public decimal Total { get; private set; } = 0;
        public string DeliveryAddress { get; private set; }
        public Order() { }

        public Order(Guid clientId, string clientName, List<Product> products, string deliveryAddress)
        {
            ClientId = clientId;
            Products = products;
            DeliveryAddress = deliveryAddress;
            CreatedBy = clientName;
            EditedBy = clientName;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            foreach (var product in products)
            {
                Total += product.Price;
            }

            Validation();
        }

        private void Validation()
        {
            var contract = new Contract<Order>()
                .IsNotNull(ClientId, "ClientId")
                .IsNotNull(Products, "Products");
            AddNotifications(contract);
        }
    }
}