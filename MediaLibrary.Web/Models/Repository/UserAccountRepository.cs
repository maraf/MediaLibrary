using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaLibrary.Web.Models.Domain;
using System.Data;

namespace MediaLibrary.Web.Models.Repository
{
    public class UserAccountRepository : IRepository<UserAccount>
    {
        DataContext db = new DataContext();

        public IQueryable<UserAccount> GetList()
        {
            return db.Users.AsQueryable();
        }

        public UserAccount Get(int id)
        {
            return db.Users.FirstOrDefault(u => u.ID == id);
        }

        public void Add(UserAccount item)
        {
            db.Users.Add(item);
            db.SaveChanges();
        }

        public void Update(UserAccount item)
        {
            db.Users.Attach(item);
            db.Entry<UserAccount>(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(UserAccount item)
        {
            db.Users.Remove(item);
            db.SaveChanges();
        }
    }
}