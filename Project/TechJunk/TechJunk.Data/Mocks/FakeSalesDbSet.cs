namespace TechJunk.Web.Tests.Mocks
{
    using System.Linq;
    using TechJunk.Models.EntityModels;

    public class FakeSalesDbSet : FakeDbSet<Sale>
    {
        public override Sale Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(sale => sale.Id == wantedId);
        }
    }
}
