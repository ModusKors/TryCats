using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Finder;
using DataAccessLayer.Repository;

namespace DataAccessLayer
{
    public class UnitOfWork
    {
        private CatsContext _catsContext;

        private CatRepository _catRepository;
        private CatFinder _catFinder;

        public UnitOfWork(CatsContext catsContext)
        {
            _catsContext = catsContext;
        }

        public CatRepository Cats
        {
            get
            {
                if (_catRepository == null)
                    _catRepository = new CatRepository(_catsContext);
                return _catRepository;
            }
        }

        public CatFinder CatsFinder
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
