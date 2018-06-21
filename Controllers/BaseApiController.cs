using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Filters;
using API.Models;
using API.Service; //.UserProfile;
//using YPAPI.Core.DBEntity;

namespace API.Controllers
{

    [CustomExceptionFilterAttribute]
   // [RESTAuthorize]
    public class BaseApiController : ApiController
    {
       // IUserProfileService _userpofileService = new UserProfileService();
        
        public int CurrentUserID()
        {
            int userid = 0;
            try
            {
                if (System.Web.HttpContext.Current.User != null)
                    Int32.TryParse(System.Web.HttpContext.Current.User.Identity.Name, out userid);
            }
            catch (Exception e) { }

            return userid;
        }
        //[Route("api/User/CurrentUserProfile")]
        //[AcceptVerbs("GET")]
        //public USER_PROFILE CurrentUserProfile()
        //{
            
        //    int userid = CurrentUserID();
        //    if (userid > 0)
        //    {
        //        return _userpofileService.GetUserProfileByID(userid);
        //    }
        //    else
        //        return null;
        //}
    }
}
