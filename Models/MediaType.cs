namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MediaType")]
    public partial class MediaType
    {

        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? GroupHeadId { get; set; }

       
        //public virtual ICollection<Media> Media { get; set; }
    }
}
