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
    public class CatService : IService<Cat>
    {
        private IRepository<Cat> _iRepository;
        private ICatFinder _iCatFinder;
        private IUnitOfWork _iUnitOfWork;

        public CatService(IRepository<Cat> iRepository, ICatFinder iCatFinder, IUnitOfWork iUnitOfWork)
        {
            _iRepository = iRepository;
            _iCatFinder = iCatFinder;
            _iUnitOfWork = iUnitOfWork;
        }

        public Task<List<Cat>> Get()
        {
            return _iRepository.GetAll();
        }

        public Task<int> Count()
        {
            return _iCatFinder.Count();
        }

        public Task<Cat> Get(int id)
        {
            return _iRepository.Get(id);
        }

        public Task<Cat> Get(string name)
        {
            return _iCatFinder.FindByName(name);
        }

        public async Task Post(Cat cat)
        {
            _iRepository.Create(cat);
            await _iUnitOfWork.Commit();
        }

        public async Task<bool> Put(Cat cat)
        {
            var getCat = await _iRepository.Get(cat.Id);
            if (getCat != null)
            {
                //_iRepository.Update(cat);
                _iRepository.Delete(cat.Id);
                _iRepository.Create(cat);
                await _iUnitOfWork.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            Cat cat = await _iRepository.Get(id);
            if (cat == null)
            {
                return false;
            }
            else
            {
                _iRepository.Delete(id);
                await _iUnitOfWork.Commit();
                return true;
            }
        }
    }
}