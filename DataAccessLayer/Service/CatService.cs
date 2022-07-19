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

namespace DataAccessLayer.Service
{
    public class CatService : ICatService
    {
        public IRepository<Cat> iRepository { get; }
        public ICatFinder iCatFinder { get; }
        public IUnitOfWork iUnitOfWork { get; }

        public CatService(IRepository<Cat> iRepository, ICatFinder iCatFinder, IUnitOfWork iUnitOfWork)
        {
            this.iRepository=iRepository;
            this.iCatFinder=iCatFinder;
            this.iUnitOfWork=iUnitOfWork;
        }
    }
}
