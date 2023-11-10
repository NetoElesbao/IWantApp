// essa é uma classe base para Product e Category


using Flunt.Notifications;

namespace IWantApp.Models.Products.Base
{
    public abstract class Entity : Notifiable<Notification>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; protected set; }
        public string CreatedBy { get; set; } // quem criou 
        public DateTime CreatedOn { get; set; } // quando criou
        public string EditedBy { get; set; } // quem editou
        public DateTime EditedOn { get; set; } // quando editou

    }
}