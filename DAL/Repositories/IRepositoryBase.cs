using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFPT.DAL.Repositories
{
    public interface IRepositoryBase<T> where T: class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(object id);
        IQueryable<T> GetAll();
    }
}
