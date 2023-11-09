using Flunt.Validations;
using IWantApp.Models.Orders;
using IWantApp.Models.Products.Base;

namespace IWantApp.Models.Products
{
    public class Product : Entity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public bool HasStock { get; set; } // se há produto no estoque 
        public decimal Price { get; private set; }
        public ICollection<Order> Orders { get; private set; }

        public Product() { }
        public Product(string name, Category category, string description, bool hasStock, string userId, decimal price)
        {
            Name = name;
            Category = category;
            Description = description;
            HasStock = hasStock;
            Price = price;

            CreatedBy = userId;
            CreatedOn = DateTime.Now;
            EditedBy = userId;
            EditedOn = DateTime.Now;

            Validation();
        }
        private void Validation()
        {
            var contract = new Contract<Product>()
                .IsNotNullOrEmpty(Name, "Name")
                .IsGreaterOrEqualsThan(Name, 4, "Name")
                .IsNotNull(Category, "Category", "Category not found")
                .IsNotNullOrEmpty(Description, "Description")
                .IsGreaterOrEqualsThan(Description, 4, "Description")
                .IsGreaterOrEqualsThan(Price, 1, "Price")
                .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
                .IsNotNullOrEmpty(EditedBy, "EditedBy");
            AddNotifications(contract);
        }

        public void EditProduct(string name, Guid categoryId, string description, bool hasStock, string UserId)
        {
            Name = name;
            CategoryId = categoryId;
            Description = description;
            HasStock = hasStock;
            EditedBy = UserId;
            EditedOn = DateTime.Now;

            Validation();
        }
    }
}