namespace TechJunk.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual ICollection<Interest> Interests { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<Message> RecievedMessages { get; set; }

        [InverseProperty("Reciever")]
        public virtual ICollection<Message> SentMessages { get; set; }

        public bool IsBanned { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
    }
}
