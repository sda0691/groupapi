using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace API.Service
{
    public interface ILoggerService
    {
        void InsertLog(Exception objErr,string username);
        void InsertLog(string message, string stacktrace, string url,string username);
    }
}
