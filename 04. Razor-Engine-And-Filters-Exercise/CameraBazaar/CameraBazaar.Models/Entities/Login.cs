namespace CameraBazaar.Models.Entities
{
    using System;

    public class Login
    {
        public int Id { get; set; }

        public string SessionId { get; set; }

        public virtual User User { get; set; }

        public bool IsActive { get; set; }

        public DateTime LoginStamp { get; set; }
    }
}
