using NewsFPT.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFPT.DAL.UnitOfWork
{
    
    public interface IUnitOfWork : IDisposable
    {      
        // get repository of any model
        public IRepositoryBase<T> GetRepository<T>() where T : class;
        public void Commit();
    }
}
