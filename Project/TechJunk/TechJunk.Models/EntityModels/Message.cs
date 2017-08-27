namespace TechJunk.Models.EntityModels
{
    using System;

    public class Message
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Description { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public virtual ApplicationUser Reciever { get; set; }

        public bool HasSenderDeleted { get; set; }

        public bool HasRecieverDeleted { get; set; }

        public DateTime DateMade { get; set; }
    }
}
