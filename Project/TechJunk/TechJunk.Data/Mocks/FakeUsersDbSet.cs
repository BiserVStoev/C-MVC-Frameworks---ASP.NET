namespace TechJunk.Data.Mocks
{
    using System.Linq;
    using TechJunk.Models.EntityModels;
    using TechJunk.Web.Tests.Mocks;
    public class FakeUsersDbSet : FakeDbSet<ApplicationUser>
    {
        public override ApplicationUser Find(params object[] keyValues)
        {
            string wantedId = (string)keyValues[0];
            return this.Set.FirstOrDefault(user => user.Id == wantedId);
        }
    }
}
