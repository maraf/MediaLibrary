using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaLibrary.Web.Models.Domain;
using System.Data.Entity;

namespace MediaLibrary.Web.Models
{
    public class DataContext : DbContext
    {
        public DbSet<UserAccount> Users { get; set; }

        public DbSet<MediaLibrary.Web.Models.Domain.Database> Databases { get; set; }

        public DbSet<DatabaseRevision> DatabaseRevisions { get; set; }
    }
}