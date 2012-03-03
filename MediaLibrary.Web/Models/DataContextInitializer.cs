using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MediaLibrary.Web.Models;

namespace MediaLibrary.Web.Models
{
    public class DataContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            context.SaveChanges();
        }
    }
}