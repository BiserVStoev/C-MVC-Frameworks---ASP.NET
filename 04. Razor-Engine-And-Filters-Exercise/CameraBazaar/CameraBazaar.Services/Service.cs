namespace CameraBazaar.Services
{
    using CameraBazaar.Data;

    public class Service
    {
        protected Service()
        {
            this.Context = new CameraBazaarContext();
        }

        protected CameraBazaarContext Context { get; }
    }
}
