namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using API.Service;

    [Table("ApiTest")]
    public partial class ApiTest
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        
        
    }
}
