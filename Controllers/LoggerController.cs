using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using API.Service;
//using YPAPI.Service.Configs;
//using YPAPI.Common;

namespace YPAPI.Controllers
{
    public class LoggerController : ApiController // BaseApiController
    {
         ILoggerService _loggerServie = new LoggerService();


        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult InsertLog(string message,string stacktrace)
        {
            Exception ex = new Exception(message);
            string user = "1"; // CurrentUserID().ToString();
            _loggerServie.InsertLog(message, stacktrace,"api/Logger", user);

            return Ok("ok");
        }


    }
}
