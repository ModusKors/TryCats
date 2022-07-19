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
    public interface ICatService2 : IRepository<Cat>, ICatFinder, IUnitOfWork
    {

    }
}
