using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Models.DataContext
{
    public class KurumsalDBContext : DbContext
    {
        public KurumsalDBContext() : base("KurumsalDB")
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SiteIdentity> SiteIdentities { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<KurumsalDBContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}