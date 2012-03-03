using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaLibrary.Web.Models.Domain;
using System.Data;

namespace MediaLibrary.Web.Models.Repository
{
    public class DatabaseRevisionRepository : IRepository<DatabaseRevision>
    {
        private DataContext db = new DataContext();

        public IQueryable<DatabaseRevision> GetList()
        {
            return db.DatabaseRevisions;
        }

        public DatabaseRevision Get(int id)
        {
            return db.DatabaseRevisions.FirstOrDefault(dr => dr.ID == id);
        }

        public void Add(DatabaseRevision item)
        {
            db.DatabaseRevisions.Add(item);
            db.SaveChanges();
        }

        public void Update(DatabaseRevision item)
        {
            db.DatabaseRevisions.Attach(item);
            db.Entry<DatabaseRevision>(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(DatabaseRevision item)
        {
            db.DatabaseRevisions.Remove(item);
            db.SaveChanges();
        }
    }
}