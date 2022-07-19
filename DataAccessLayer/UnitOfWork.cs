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

        private CatRepository _catRepository;
        private CatFinder _catFinder;

        public UnitOfWork(CatsContext catsContext)
        {
            _catsContext = catsContext;
        }

        public IRepository<Cat> Cats
        {
            get
            {
                if (_catRepository == null)
                    _catRepository = new CatRepository(_catsContext);
                return _catRepository;
            }
        }

        public ICatFinder CatsFinder
        {
            get
            {
                if (_catFinder == null)
                    _catFinder = new CatFinder(_catsContext);
                return _catFinder;
            }
        }

        public Task<int> Commit()
        {
            return _catsContext.SaveChangesAsync();
        }
    }
}
