namespace ControlFlow.Web.Handlers
{
    using System.Web;

    public class UserInfoHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string userAgent = context.Request.UserAgent;
            string userIp = context.Request.UserHostAddress;
            context.Response.Write($"<p>User agent: {userAgent} </br> User Ip address: {userIp}.</p>");
        }

        public bool IsReusable { get; }
    }
}