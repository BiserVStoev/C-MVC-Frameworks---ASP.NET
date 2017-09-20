namespace TechJunk.Services
{
    using TechJunk.Data.Interfaces;

    public abstract class Service
    {
        public Service(ITechJunkDbContext context)
        {
            this.Context = context;
        }

        protected ITechJunkDbContext Context { get; }
    }
}
