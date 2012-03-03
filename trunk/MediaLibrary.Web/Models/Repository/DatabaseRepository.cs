using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaLibrary.Web.Models.Domain;
using System.Data;

namespace MediaLibrary.Web.Models.Repository
{
    public class DatabaseRepository : IRepository<Database>
    {
        private DataContext db = new DataContext();

        public IQueryable<Database> GetList()
        {
            return db.Databases;
        }

        public Database Get(int id)
        {
            return db.Databases.FirstOrDefault(d => d.ID == id);
        }

        public void Add(Database item)
        {
            db.Databases.Add(item);
            db.SaveChanges();
        }

        public void Update(Database item)
        {
            db.Databases.Attach(item);
            db.Entry<Database>(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Database item)
        {
            db.Databases.Remove(item);
            db.SaveChanges();
        }
    }
}