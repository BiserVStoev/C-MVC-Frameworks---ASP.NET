namespace TechJunk.Web.Tests.Mocks
{
    using System.Linq;
    using TechJunk.Models.EntityModels;

    public class FakeFeedbacksDbSet : FakeDbSet<Feedback>
    {
        public override Feedback Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(car => car.Id == wantedId);
        }
    }
}
