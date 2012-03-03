using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaLibrary.Web.Models.Domain;

namespace MediaLibrary.Web.Models.Repository
{
    public interface IRepository<T> where T : BaseObject
    {
        IQueryable<T> GetList();

        T Get(int id);

        void Add(T item);

        void Update(T item);

        void Delete(T item);
    }
}