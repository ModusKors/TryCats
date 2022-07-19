using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Finder;
using BusinessLogicLayer.Repository;

namespace BusinessLogicLayer.Service
{
    public interface ICatService
    {
        IRepository<Cat> iRepository { get; }
        ICatFinder iCatFinder { get; }
        IUnitOfWork iUnitOfWork { get; }
    }
}
