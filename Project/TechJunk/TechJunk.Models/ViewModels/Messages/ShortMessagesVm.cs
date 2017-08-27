namespace TechJunk.Models.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;

    public class ShortMessagesVm
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string SenderId { get; set; }

        public string RecieverId { get; set; }

        public string DateMade { get; set; }

        [Display(Name = "Sender")]
        public string SenderEmail { get; set; }
    }
}
