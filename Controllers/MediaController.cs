using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Service;
namespace API.Controllers
{

    public class MediaController : ApiController
    {
        MediaService _mediaService = new MediaService();
        public IHttpActionResult GetAllNews()
        {
            var news = _mediaService.GetAllMedia();
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }
    }
}
