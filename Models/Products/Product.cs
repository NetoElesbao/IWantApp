using IWantApp.Models.Products.Base;


namespace IWantApp.Models.Products
{
    public class Product : Entity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public bool HasStock { get; set; } // se há produto no estoque 
    }
}