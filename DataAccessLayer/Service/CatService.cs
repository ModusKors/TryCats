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

namespace DataAccessLayer.Service
{
    public class CatService : ICatService
    {
        private CatsContext _catsContext;

        private IRepository<Cat> _catRepository;
        private ICatFinder _catFinder;
        private IUnitOfWork _unitOfWork;

        public CatService(CatsContext catsContext)
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

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = new UnitOfWork(_catsContext);
                return _unitOfWork;
            }
        }
    }
}
