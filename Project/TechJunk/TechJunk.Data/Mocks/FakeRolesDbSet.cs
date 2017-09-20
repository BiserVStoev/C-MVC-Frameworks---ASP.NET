namespace TechJunk.Data.Mocks
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using TechJunk.Web.Tests.Mocks;

    public class FakeRolesDbSet: FakeDbSet<IdentityRole>
    {
        public FakeRolesDbSet()
        {
            this.Add(new IdentityRole("Admin"));
            this.Add(new IdentityRole("JunkLover"));
        }

    }
}
