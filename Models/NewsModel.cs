namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
   // [Table("News")]
    public  class NewsModel
    {
        public int Id { get; set; }

        public int GroupHeadId { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public string Note { get; set; }

        public int? WhoCreated { get; set; }

        public DateTime? WhenCreated { get; set; }

        public int? Viewer { get; set; }

        public int? Comment { get; set; }
        public string VideoPath { get; set; }

        public IEnumerable<NewsImage> NewsImage { get; set; }
    }
}
