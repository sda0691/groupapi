using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using System.Linq.Expressions;
using API.Models;
using API.Common;
namespace API.Service
{
    public class LoggerService : ILoggerService
    {
        private IGenericRepository<Logger> _loggerRepository;
        private System.Data.Entity.DbContext dbContext = new GroupContext();

        protected IGenericRepository<Logger> loggerRepository
        {
            get
            {
                return _loggerRepository == null ? new GenericRepository<Logger>(dbContext) : _loggerRepository;
            }
            set
            {
                _loggerRepository = value;
            }
        }

        public void InsertLog(string message, string stacktrace, string url, string username)
        {
            //string url = System.Web.HttpContext.Current.Request.Url.ToString().Substring(0, 255);

            var logger = new Logger();
            logger.MessageType = "Error";
            logger.Message = message;
            logger.StackTrace = stacktrace;
            logger.URL = url;
            logger.LogDate = System.DateTime.Now;
            logger.UserName = username;
            loggerRepository.Insert(logger);
            Save();

            //send email
            SendEmailNotification(message, stacktrace, url, username);
        }

        public void InsertLog(Exception objErr, string username)
        {
            string custmessage = string.Empty;

            string url = System.Web.HttpContext.Current.Request.Url.ToString();

            Exception baseErr = objErr.GetBaseException();
            string trace = baseErr.StackTrace;
            //baseErr.InnerException.Message


            StringBuilder messageBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(custmessage) && custmessage != baseErr.Message)
                messageBuilder.Append(custmessage);
            messageBuilder.Append(baseErr.Message);

            string postdata = string.Empty;

            try
            {
                for (int i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++)
                    postdata = postdata + System.Web.HttpContext.Current.Request.QueryString.Keys[i] + "=" + System.Web.HttpContext.Current.Request.QueryString[i] + ",";
                if (postdata.Length > 0)
                    messageBuilder.AppendLine("(" + postdata + ")");
            }
            catch (Exception ex) { }

            InsertLog(messageBuilder.ToString(), trace, url, username);
            /*
            var logger = new Logger();
            logger.MessageType = "Error";
            logger.Message = messageBuilder.ToString();
            logger.StackTrace = trace;
            logger.URL = url;
            logger.LogDate = System.DateTime.Now;
            logger.UserName = username;
            loggerRepository.Insert(logger);
            Save();
            */
        }
        protected void Save()
        {
            dbContext.SaveChanges();
        }

        public void SendEmailNotification(string message, string stacktrace, string url, string username)
        {
            try
            {
                //string mailto = "jwu@cdsglobal.ca";
                //string mailfrom = "cmishelp@cdsglobal.ca";
                string emailconfig = System.Configuration.ConfigurationManager.AppSettings["ErrorNotificationEnabled"];
                if (emailconfig == null) emailconfig = "";

                if ( !emailconfig.ToLower().Equals("false") )
                {
                    string mailadmin = System.Configuration.ConfigurationManager.AppSettings["mailAdmin"];
                    string Subject = "YPCSR Site Error Log";

                    StringBuilder messageBuilder = new StringBuilder();
                    if (message == null) message = string.Empty;
                    if (stacktrace == null) stacktrace = string.Empty;
                    if (url == null) url = string.Empty;

                    messageBuilder.AppendLine("Error Caught on " + DateTime.Now);
                    messageBuilder.AppendLine("User Name: " + username);
                    messageBuilder.AppendLine("Error in:" + url);
                    messageBuilder.AppendLine("Error Message:" + message);
                    messageBuilder.AppendLine("Stack Trace:" + stacktrace);

                    CMISEmailSender email = new CMISEmailSender("", mailadmin, mailadmin, Subject, messageBuilder.ToString());
                    email.BodyFormat = "Text";

                    bool result = email.Send();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
