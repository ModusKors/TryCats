using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Finder;
using BusinessLogicLayer.Repository;
using BusinessLogicLayer.Service;
using DataAccessLayer.Finder;
using DataAccessLayer.Repository;

namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private CatsContext _catsContext;

        public UnitOfWork(CatsContext catsContext)
        {
            _catsContext = catsContext;
        }

        public Task<int> Commit()
        {
            return _catsContext.SaveChangesAsync();
        }
    }
}
