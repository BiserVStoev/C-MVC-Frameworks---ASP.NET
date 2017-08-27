namespace TechJunk.Web.Tests.Mocks
{
    using System.Data.Entity;
    using TechJunk.Data.Interfaces;
    using TechJunk.Data.Mocks;
    using TechJunk.Models.EntityModels;

    public class FakeTechJunkDbContext : ITechJunkDbContext
    {
        public FakeTechJunkDbContext()
        {
            this.Feedbacks = new FakeFeedbacksDbSet();
            this.Messages = new FakeMessagesDbSet();
            this.Interests = new FakeInterestsDbSet();
            this.Sales = new FakeSalesDbSet();
            this.Users = new FakeUsersDbSet();
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
