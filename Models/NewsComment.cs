namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewsComment")]
    public partial class NewsComment
    {
        public int Id { get; set; }

        public int? NewsId { get; set; }

        public string Note { get; set; }

        public int? WhoCreated { get; set; }

        public DateTime? WhenCreated { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual News News { get; set; }
    }
}
