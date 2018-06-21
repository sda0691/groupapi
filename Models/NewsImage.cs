namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("NewsImage")]
    public partial class NewsImage
    {
        public int Id { get; set; }

        public int? NewsId { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string ImagePath { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual News News { get; set; }
    }
}
