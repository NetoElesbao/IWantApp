
using Flunt.Validations;
using IWantApp.Models.Products.Base;


namespace IWantApp.Models.Products
{
    public class Category : Entity
    {
        public Category(string name, string createdBy, string editedBy)
        {
            Name = name;
            CreatedBy = createdBy;
            CreatedOn = DateTime.Now;
            EditedBy = editedBy;
            EditedOn = DateTime.Now;

            Validation();
        }

        public void Validation()
        {
            var contract = new Contract<Category>()
                         .IsNotNullOrEmpty(Name, "Name")
                         .IsGreaterOrEqualsThan(Name, 4, "Name")
                         .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
                         .IsNotNullOrEmpty(EditedBy, "EditedBy");
            AddNotifications(contract);
        }

        public void EditCategory(string name)
        {
            Name = name;

            Validation();
        }


    }
}