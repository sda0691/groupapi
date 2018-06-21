using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Service;
//using YPAPI.Service.CodeLookups;

namespace API.Filters
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {

            var _loggerService = new LoggerService();
            //var _lookupService = new CodeLookupService();

            string user = (System.Web.HttpContext.Current.User != null ? System.Web.HttpContext.Current.User.Identity.Name : "");
            _loggerService.InsertLog(context.Exception, user);

            int code = 101;
            //string message = _lookupService.GetErrorCodeDescription(code.ToString());

            var badresult = new
            {
                Code = code,
                Message = "ERROR..."
            };

            context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, badresult);
        }
    }
}