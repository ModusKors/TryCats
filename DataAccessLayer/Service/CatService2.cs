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
    public class CatService2 : ICatService2
    {
        public Task<List<Cat>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Cat> Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Cat item)
        {
            throw new NotImplementedException();
        }

        public void Update(Cat item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<Cat?> FindCatById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Cat?> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cat>> FindAllCatsByName(string name)
        {
            throw new NotImplementedException();
        }

        public IRepository<Cat> Cats { get; }
        public ICatFinder CatsFinder { get; }
        public Task<int> Commit()
        {
            throw new NotImplementedException();
        }
    }
}
