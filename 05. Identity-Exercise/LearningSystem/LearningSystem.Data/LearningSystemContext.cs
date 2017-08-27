namespace LearningSystem.Data
{
    using System.Data.Entity;
    using LearningSystem.Models.EntityModels;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class LearningSystemContext : IdentityDbContext<ApplicationUser>
    {
        public LearningSystemContext()
            : base("data source=.;initial catalog=LearningSystemDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses{ get; set; }

        public DbSet<Articles> Articles{ get; set; }

        public static LearningSystemContext Create()
        {
            return new LearningSystemContext();
        }
    }
}