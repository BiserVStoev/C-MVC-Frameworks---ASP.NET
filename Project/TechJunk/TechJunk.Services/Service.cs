namespace TechJunk.Services
{
    using TechJunk.Data;

    public class Service
    {
        public Service()
        {
            this.Context = new TechJunkContext();
        }

        protected TechJunkContext Context { get; }
    }
}
