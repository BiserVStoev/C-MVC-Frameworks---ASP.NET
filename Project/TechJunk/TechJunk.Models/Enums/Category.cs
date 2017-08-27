namespace TechJunk.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum Category
    {
        [Display(Name = "USB Memory Sticks")]
        UsbMemorySticks = 1,
        [Display(Name = "Mouse and Keyboards")]
        MouseAndKeyboards = 2,
        Speakers = 3,
        Charges = 4,
        Cables = 5,
        [Display(Name = "PC's")]
        PCs = 6,
        Laptops = 7,
        Phones = 8,
        Other = 9
    }
}
