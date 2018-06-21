namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using API.Service;
    [Table("GroupHead")]
    public partial class GroupHead

    {
        
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        //public virtual News News { get; set; }

        //public virtual ICollection<User> Users { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<News> News { get; set; }

        //public virtual ICollection<User> Users { get; set; }


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
