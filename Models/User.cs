namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Member")]
    public partial class Member
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        public virtual News News { get; set; }
        //public virtual GroupHead GroupHead { get; set; }
    }
}
