namespace CameraBazaar.Data
{
    using System.Data.Entity;
    using CameraBazaar.Models.Entities;

    public class CameraBazaarContext : DbContext
    {
        public CameraBazaarContext()
            : base("CameraBazaarContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Camera> Cameras { get; set; }

        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camera>().Property(camera => camera.Price).HasPrecision(16, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}