 namespace ControlFlow.Web.Modules
{
    using System;
    using System.IO;
    using System.Text;
    using System.Web;
    public class LoggerModule : IHttpModule
    {
        private const string FileName = "requestLogs.txt";

        private DateTime _incomingTime;
        private DateTime _outgoingTime;
        private HttpApplication _context;
        private Uri _url;

        public void Init(HttpApplication context)
        {
            this._context = context;
            context.BeginRequest += this.SetIncomeTime;
            context.BeginRequest += this.SetUrlOfRequest;
            context.EndRequest += this.SetOutgoingTime;
            context.EndRequest += this.LogRequest;
        }

        private void LogRequest(object sender, EventArgs e)
        {
            var timeSpanOfRequest = this._outgoingTime - this._incomingTime;
            var pathToFolder = this._context.Server.MapPath("~/logs");
            var fullPath = Path.Combine(pathToFolder, FileName);

            StringBuilder requestLog = new StringBuilder();
            requestLog.AppendLine("Request log: ");
            requestLog.AppendLine($"Request income time: {this._incomingTime}");
            requestLog.AppendLine($"Request outcome time: {this._outgoingTime}");
            requestLog.AppendLine($"Request timespan: {timeSpanOfRequest}");
            requestLog.AppendLine($"Request URL: {this._url}");
            requestLog.AppendLine(new string('-', 40));
            
            File.AppendAllText(fullPath, requestLog.ToString());
        }

        private void SetUrlOfRequest(object sender, EventArgs e)
        {
            this._url = this._context.Request.Url;
        }

        private void SetOutgoingTime(object sender, EventArgs e)
        {
            this._outgoingTime = DateTime.Now;
        }

        private void SetIncomeTime(object sender, EventArgs e)
        {
            this._incomingTime = DateTime.Now;
        }

        public void Dispose()
        {
        }
    }
}