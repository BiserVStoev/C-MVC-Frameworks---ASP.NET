namespace TechJunk.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TechJunk.Data.Interfaces;
    using TechJunk.Models.EntityModels;

    public class TechJunkContext : IdentityDbContext<ApplicationUser>, ITechJunkDbContext
    {
        public TechJunkContext()
            : base("data source=.;initial catalog=TechJunkDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        public virtual DbSet<Interest> Interests { get; set; }

        public virtual DbSet<Feedback> Feedbacks { get; set; }

        public static TechJunkContext Create()
        {
            return new TechJunkContext();
        }
    }
}