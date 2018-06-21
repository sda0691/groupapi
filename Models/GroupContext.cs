namespace API.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GroupContext : DbContext
    {
        public GroupContext()
            : base("name=GroupContext")
        {
        }
        
        public virtual DbSet<GroupHead> Groups { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsComment> NewsComments { get; set; }
        public virtual DbSet<NewsImage> NewsImage { get; set; }
        //public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<MediaType> MediaType { get; set; }
        public virtual DbSet<ApiTest> ApiTest { get; set; }
        public virtual DbSet<Logger> Logger { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
