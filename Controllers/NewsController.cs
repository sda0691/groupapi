using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Service;
using API.Models;
using System.Web;
using API.Common;
namespace API.Controllers
{

    public class NewsController : BaseApiController //ApiController
    {
        NewsService _newsService = new NewsService();
        NewsImageService _imageService = new NewsImageService();
        public IHttpActionResult GetAllNews()
        {
            
            var news = _newsService.GetAllNews();
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }
        public IHttpActionResult GetNewsByGroupId(int group)
        {
            
            var news = _newsService.GetNewsByGroupId(group);
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }
        //[Route("api/News/AddNews")]
        public IHttpActionResult GetNewsById(int id)
        {
            var news = _newsService.GetNewsById(id);
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }
        public IHttpActionResult GetAllNewsImage(string test)
        {
            var news = _imageService.GetAllNewsImage();
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        [Route("api/News/AddNews")]
        [AcceptVerbs("POST")]
       // [System.Web.Http.HttpPost]
        public IHttpActionResult AddNews(NewsModel news) //ApiTest news)  //(int id, string name) //
        //public IHttpActionResult AddNews(int id, string name) //
        {
            
            string Host = "";

            //IEnumerable<string> origin;
            //Request.Headers.TryGetValues("Origin", out origin);
            //foreach (var i in origin)
            //{
            //    Host = i.ToString();
            //}
            //if (Request.Headers.Contains("Origin"))
            //    Host = Request.Headers.GetValues("Origin");

            //var ip = CommonHelper.GetIP(request)
            //OrderTest callOrderHistory = new OrderTest();
            //callOrderHistory.ID = id;
            //news.WhenCreated = DateTime.Now;
            //int s = news.GroupHeadId;
            _newsService.AddNews(news);

            //var response = this.Request.CreateResponse<CallOrderHistory>(HttpStatusCode.Created, callOrderHistory);
            //string uri = Url.Link("DefaultApi", new { id = callOrderHistory.ID });
            //response.Headers.Location = new Uri(uri);
            //return response;
            //return "SUCCESS";
            return Ok(news);
        }
        [Route("api/News/update")]
        [AcceptVerbs("POST")]
        [HttpGet]
        public IHttpActionResult UpdateNews(NewsModel news) 
        {
            _newsService.UpdateNews(news);
            return Ok(news);
        }

        [Route("api/News/Delete")]
        [AcceptVerbs("POST")]
        [HttpGet]
        // [System.Web.Http.HttpPost]
        public IHttpActionResult DeleteNewsById(int id) //ApiTest news)  //(int id, string name) //
        //public IHttpActionResult AddNews(int id, string name) //
        {
            _newsService.DeleteNewsByNewsId(id);


            return Ok("ok");
        }

        [Route("api/News/update")]
        [AcceptVerbs("POST")]
        [HttpGet]
        // [System.Web.Http.HttpPost]
        public IHttpActionResult UpdateNewsById(int id) //ApiTest news)  //(int id, string name) //
        //public IHttpActionResult AddNews(int id, string name) //
        {
            _newsService.DeleteNewsByNewsId(id);


            return Ok("ok");
        }
        [HttpPost]
        [Route("api/news/upload")]
        public HttpResponseMessage uploadImage()
        {
            var request = HttpContext.Current.Request;

            if (Request.Content.IsMimeMultipartContent())
            {
                if (request.Files.Count > 0)
                {
                    //var postedFile = request.Files.Get("file");
                    var postedFile = request.Files[0];
                    var folder = request.Params["folder"];
                    string root = HttpContext.Current.Server.MapPath("~/Images");
                    root = root + "/" +folder+ "/" + postedFile.FileName;
                    postedFile.SaveAs(root);
                    //Save post to DB
                    return Request.CreateResponse(HttpStatusCode.Found, new
                    {
                        error = false,
                        status = "created",
                        path = root
                    });

                }
            }

            return null;
        }
        [Route("api/News/AddNews1")]
        [AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult AddNews1(News news1) //ApiTest news)  //(int id, string name) //
        {
            //OrderTest callOrderHistory = new OrderTest();
            //callOrderHistory.ID = id;
            //news.WhenCreated = DateTime.Now;
            //_newsService.AddNews(news);

            //var response = this.Request.CreateResponse<CallOrderHistory>(HttpStatusCode.Created, callOrderHistory);
            //string uri = Url.Link("DefaultApi", new { id = callOrderHistory.ID });
            //response.Headers.Location = new Uri(uri);
            //return response;
            //return "SUCCESS";
            return Ok("dfdf");
        }

    }
}
