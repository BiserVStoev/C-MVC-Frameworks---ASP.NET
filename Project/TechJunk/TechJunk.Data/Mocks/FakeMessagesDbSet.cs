namespace TechJunk.Web.Tests.Mocks
{
    using System.Linq;
    using TechJunk.Models.EntityModels;

    public class FakeMessagesDbSet : FakeDbSet<Message>
    {
        public override Message Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(message => message.Id == wantedId);
        }
    }
}
