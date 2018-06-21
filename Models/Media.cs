namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Media")]
    public partial class Media
    {
        public int Id { get; set; }

        [StringLength(1000)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string SubTitle { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        public int? MediaTypeId { get; set; }

        public DateTime? DateLastPlayed { get; set; }

        public DateTime? WhenCreated { get; set; }

        public int? WhoCreated { get; set; }

        public int? GroupHeadId { get; set; }
        public string MediaPath { get; set; }

        public virtual MediaType MediaType { get; set; }
    }
}
