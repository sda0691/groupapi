using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Service;
namespace API.Controllers
{

    public class MemberController : ApiController
    {
        MemberService _memberService = new MemberService();
        public IHttpActionResult GetAllMember()
        {
            var member = _memberService.GetAllMember();
            
            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }
    }
}
