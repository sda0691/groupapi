using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace API.Common
{
    public class CommonHelper
    {
        public static string GetIP(HttpRequestBase request /*HttpRequestBase request*/)
        {
            string ip = request.Headers["X-Forwarded-For"]; // AWS compatibility

            if (string.IsNullOrEmpty(ip))
            {
                ip = request.UserHostAddress;
            }

            return ip;

        }
    }
}