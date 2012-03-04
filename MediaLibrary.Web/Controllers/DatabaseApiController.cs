using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaLibrary.Web.Models.Domain;
using MediaLibrary.Web.Models.Repository;
using Microsoft.Practices.Unity;
using System.Text;

namespace MediaLibrary.Web.Controllers
{
    public class DatabaseApiController : MediaLibrary.Web.Mvc.Controller
    {
        [Dependency]
        IRepository<Database> Databases { get; set; }

        [Dependency]
        IRepository<DatabaseRevision> Revisions { get; set; }

        public string Get(string identifier)
        {
            DatabaseRevision revision = Revisions.GetList().Where(r => r.Database.PublicIdentifier == identifier).OrderByDescending(r => r.Revision).FirstOrDefault();
            if (revision != null)
                return revision.Content;

            return null;
        }

        [HttpPost]
        public string Update(string identifier)
        {
            byte[] buffer = new byte[Request.TotalBytes];
            Request.InputStream.Read(buffer, 0, Request.TotalBytes);

            Database database = Databases.GetList().FirstOrDefault(d => d.PublicIdentifier == identifier);
            if (database != null)
            {
                DatabaseRevision revision = new DatabaseRevision
                {
                    DatabaseID = database.ID,
                    Content = Encoding.UTF8.GetString(buffer, 0, buffer.Length),
                    Revision = database.Revision + 1,
                    Timestamp = DateTime.Now
                };
                Revisions.Add(revision);

                database.Revision = revision.Revision;
                Databases.Update(database);

                return revision.Content;
            }

            //Request.InputStream
            return null;
        }
    }
}
