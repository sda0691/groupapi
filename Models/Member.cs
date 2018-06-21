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

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Addr1 { get; set; }

        [StringLength(500)]
        public string Addr2 { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(10)]
        public string Province { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string Phone1 { get; set; }

        [StringLength(50)]
        public string Phone2 { get; set; }

        [StringLength(500)]
        public string Avatar { get; set; }

        [StringLength(100)]
        public string GroupType { get; set; }

        public int? GroupTypeCode { get; set; }

        [StringLength(50)]
        public string City { get; set; }
    }
}
