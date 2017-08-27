namespace CarDealerApp.Filters
{
    using System;
    using System.IO;
    using System.Web.Mvc;
    using CarDealerApp.Security;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var logTimeStamp = DateTime.Now;
            var ipAddress = filterContext.HttpContext.Request.UserHostAddress;
            var username = "Anonymous";
            var requestCookie = filterContext.HttpContext.Request.Cookies["sessionId"];
            if (requestCookie != null)
            {
                string sessionId = requestCookie.Value;
                if (AuthenticationManager.IsAuthenticated(sessionId))
                {
                    username = AuthenticationManager.GetAuthenticatedUser(sessionId).Username;
                }
            }

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionNme = filterContext.ActionDescriptor.ActionName;
            var exception = filterContext.Exception;

            var logMessage = string.Empty;
            if (exception == null)
            {
                logMessage = $"{logTimeStamp} - {ipAddress} - {username} - {controllerName}.{actionNme}{Environment.NewLine}";
            }
            else
            {
                logMessage = $"[!] {logTimeStamp} - {ipAddress} - {username} - {exception.GetType().Name} - {exception.Message}{Environment.NewLine}";
            }

            File.AppendAllText("D:\\log.txt", logMessage);
        }
    }
}