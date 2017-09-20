namespace TechJunk.Data.Interfaces
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TechJunk.Models.EntityModels;

    public interface ITechJunkDbContext
    {
        DbSet<Message> Messages { get; set; }

        DbSet<Sale> Sales { get; set; }

        DbSet<Interest> Interests { get; set; }

        DbSet<Feedback> Feedbacks { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<IdentityRole> Roles { get; set; }

        int SaveChanges();
    }
}
