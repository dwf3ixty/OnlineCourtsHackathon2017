using OnlineCourt2017.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineCourt2017.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
            //Automatic schema migration
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, OnlineCourt2017.Data.Migrations.Configuration>());
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Case> Cases { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<OfferComment> OfferComments { get; set; }
    }
}