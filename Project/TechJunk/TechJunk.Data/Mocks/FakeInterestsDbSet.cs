namespace TechJunk.Web.Tests.Mocks
{
    using System.Linq;
    using TechJunk.Models.EntityModels;

    public class FakeInterestsDbSet : FakeDbSet<Interest>
    {
        public override Interest Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(interest => interest.Id == wantedId);
        }
    }
}
