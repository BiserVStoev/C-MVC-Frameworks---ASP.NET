namespace LearningSystem.Services
{
    using LearningSystem.Data;

    public abstract class Service
    {
        public Service()
        {
            this.Context = new LearningSystemContext();
        }

        protected LearningSystemContext Context { get; }
    }
}
