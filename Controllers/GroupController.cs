using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Service;
namespace API.Controllers
{

    public class GroupController : ApiController
    {
        GroupService _groupService = new GroupService();
        public IHttpActionResult GetAllGroup()
     {
            var groups = _groupService.GetAllGroup();
            
            if (groups == null)
            {
                return NotFound();
            }

            return Ok(groups);
        }
    }
}
