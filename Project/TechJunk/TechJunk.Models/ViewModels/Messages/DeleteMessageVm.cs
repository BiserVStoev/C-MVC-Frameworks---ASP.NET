namespace TechJunk.Models.ViewModels.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DeleteMessageVm
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Description { get; set; }

        [Display(Name = "Sender")]
        public string SenderEmail { get; set; }

        [Display(Name = "Reciever")]
        public string RecieverEmail { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateMade { get; set; }
    }
}
