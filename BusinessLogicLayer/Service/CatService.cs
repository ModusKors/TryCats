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
    public interface CatService
    {
        IRepository<Cat> Cats { get; }
        ICatFinder CatsFinder { get; }
        Task<int> Commit();
    }
}
