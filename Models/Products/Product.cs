using Flunt.Validations;
using IWantApp.Models.Products.Base;


namespace IWantApp.Models.Products
{
    public class Product : Entity
    {
        public Product(string name, Guid categoryId, string description, bool hasStock, string createdBy, string editedBy)
        {
            Name = name;
            CategoryId = categoryId;
            Description = description;
            HasStock = hasStock;
            CreatedBy = createdBy;
            CreatedOn = DateTime.Now;
            EditedBy = editedBy;
            EditedOn = DateTime.Now;

            Validation();
        }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public bool HasStock { get; set; } // se há produto no estoque 

        private void Validation()
        {
            var contract = new Contract<Product>()
                .IsGreaterOrEqualsThan(Name, 4, "Name")
                .IsNotNullOrEmpty(Name, "Name")
                // .IsNotNullOrEmpty(CategoryId, "CategoryId")
                // .IsNotNullOrEmpty(HasStock, "HasStock")
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