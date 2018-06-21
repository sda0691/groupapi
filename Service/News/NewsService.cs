using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using API.Models;
using System.Web.Http.ModelBinding;

namespace API.Service
{
    public class NewsService : INewsService
    {
       
        private IGenericRepository<News> _newsRepository;

        //private IGenericRepository<CallOrderHistory> _callOrderHistoryRepository;
        //private IGenericRepository<OrderTest> _orderTestRepository;

        private System.Data.Entity.DbContext dbContext = new GroupContext(); // new FAMTest.Data.FAMEntities();//

        protected IGenericRepository<News> NewsRepository
        {
            get
            {
                return _newsRepository == null ? new GenericRepository<News>(dbContext) : _newsRepository;
            }
            set
            {
                _newsRepository = value;
            }
        }
        private IGenericRepository<NewsImage> _newsImageRepository;

        

        protected IGenericRepository<NewsImage> NewsImageRepository
        {
            get
            {
                return _newsImageRepository == null ? new GenericRepository<NewsImage>(dbContext) : _newsImageRepository;
            }
            set
            {
                _newsImageRepository = value;
            }
        }
        protected void Save()
        {
            dbContext.SaveChanges();
        }

        public IEnumerable<News> GetAllNews()
        {
            return NewsRepository.GetAll();
        }
        public IEnumerable<News> GetNewsByGroupId(int group)
        {
            Expression<Func<News, bool>> where = a => (a.GroupHeadId == group);
            var list = NewsRepository.GetMany(where);
            list = list.OrderBy("WhenCreated descending");
            return list;
        }
        public News GetNewsById(int id)
        {
            Expression<Func<News, bool>> where = a => (a.Id == id);
            var list = NewsRepository.GetMany(where);
            //list = list.OrderBy("WhenCreated descending");
            return list.FirstOrDefault();
        }
        //data update with transaction
        public void AddNews(NewsModel news/*, string Host*/)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {

                    int newNewsId = 0;
                    News test = new News();
                    test.GroupHeadId = news.GroupHeadId;
                    test.Title = news.Title;
                    test.Note = news.Note;
                    test.VideoPath = news.VideoPath;
                    test.Viewer = 0;
                    test.Comment = 0;
                    test.WhoCreated = 1;
                    test.WhenCreated = DateTime.Now; ;


                    NewsRepository.Insert(test);
                    Save();
                    newNewsId = test.Id;
                    NewsImageService aa = new NewsImageService();
                    if (news.NewsImage != null)
                    {
                        foreach (var image in news.NewsImage.ToList())
                        {
                            if (image != null)
                            {
                                image.NewsId = newNewsId;
                                image.ImagePath = "http://cmistest.indas.on.ca/group/images/" + test.GroupHeadId +"/"+ image.ImagePath;
                                NewsImageRepository.Insert(image);
                                Save();
                                //aa.AddNewsImage(image);
                                //Save();
                            }
                        }
                    }
                    transaction.Commit();

                    //return news.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex.InnerException;
                }
            }

        }
        public void UpdateNews(NewsModel news)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var newsobj = GetNewsById(news.Id);
                    if(newsobj != null)
                    {
                        newsobj.Note = news.Note;
                        newsobj.VideoPath = news.VideoPath;
                        newsobj.WhenCreated = DateTime.Now;
                       
                    }

                    NewsRepository.Update(newsobj);
                    Save();

                    NewsImageService imageService = new NewsImageService();
                    if (newsobj != null)
                    {
                        var newsImages = imageService.GetNewsImageByNewsId(newsobj.Id);
                        foreach (NewsImage images in newsImages)
                        {
                            NewsImageRepository.Delete(images);
                        }
                        Save();
                    }
                    //newNewsId = test.Id;
                    //NewsImageService aa = new NewsImageService();
                    if (news.NewsImage != null)
                    {
                        foreach (var image in news.NewsImage.ToList())
                        {
                            if (image != null)
                            {
                                image.NewsId = newsobj.Id;
                                image.ImagePath = "http://cmistest.indas.on.ca/group/images/" + newsobj.GroupHeadId + "/" + image.ImagePath;
                                NewsImageRepository.Insert(image);
                                Save();
                                //aa.AddNewsImage(image);
                                //Save();
                            }
                        }
                    }
                    transaction.Commit();

                    //return news.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex.InnerException;
                }
            }

        }
        public void DeleteNewsByNewsId(int id )
        {
            NewsImageService imageService = new NewsImageService();
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var news = GetNewsById(id);
                    if(news!=null)
                    {
                        var newsImages = imageService.GetNewsImageByNewsId(news.Id);
                        foreach (NewsImage images in newsImages)
                        {
                            NewsImageRepository.Delete(images);
                        }
                        Save();
                    }

                    NewsRepository.Delete(news);
                    Save();


                    transaction.Commit();
                    //return order.ID;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex.InnerException;
                }
            }
        }
    }
}
