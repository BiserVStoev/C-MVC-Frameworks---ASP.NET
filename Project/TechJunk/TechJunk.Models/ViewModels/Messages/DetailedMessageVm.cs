namespace TechJunk.Models.ViewModels.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DetailedMessageVm
    {
        public string Topic { get; set; }

        public string Description { get; set; }

        public string SenderId { get; set; }

        public string RecieverId { get; set; }

        [Display(Name = "Sender")]
        public string SenderEmail { get; set; }

        [Display(Name = "Reciever")]
        public string RecieverEmail { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateMade { get; set; }
    }
}
