namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using API.Service;

    [Table("News")]
    public partial class News
    {
        public int Id { get; set; }

        public int GroupHeadId { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public string Note { get; set; }

        public int WhoCreated { get; set; }

        public DateTime? WhenCreated { get; set; }

        public int? Viewer { get; set; }

        public int? Comment { get; set; }
        public string VideoPath { get; set; }

        public virtual GroupHead GroupHead { get; set; }
        //public virtual Member Member { get; set; }
        public Member Member
        {
            get
            {
                MemberService code = new MemberService();
                var item = code.GetMemberByMemberId(this.WhoCreated);
                if (item != null)
                    return item;
                else
                    return null;
            }
        }
        //[Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<NewsComment> NewsComment { get; set; }
        //public IEnumerable<News> News
        //{
        //    get
        //    {
        //        NewsService code = new NewsService();
        //        var item = code.GetAllNews();
        //        if (item != null)
        //            return item;
        //        else
        //            return null;
        //    }
        //}
        //public virtual ICollection<User> User { get; set; }
        //public IEnumerable<News> News
        //{
        //    get
        //    {
        //        NewsService code = new NewsService();
        //        var item = code.GetAllNews();
        //        if (item != null)
        //            return item;
        //        else
        //            return null;
        //    }
        //}
        //[Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<NewsImage> NewsImage { get; set; }
        //public IEnumerable<News> News
        //{
        //    get
        //    {
        //        NewsService code = new NewsService();
        //        var item = code.GetAllNews();
        //        if (item != null)
        //            return item;
        //        else
        //            return null;
        //    }
        //}
    }
}
