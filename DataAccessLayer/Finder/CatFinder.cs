using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Finder;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Finder
{
    public class CatFinder : ICatFinder
    {
        private CatsContext _catsContext;

        public CatFinder(CatsContext catsContext)
        {
            _catsContext = catsContext;
        }

        public Task<int> Count() => _catsContext.Cats.CountAsync();

        public Task<Cat?> FindCatById(int id)
        {
            return _catsContext.Cats.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Cat?> FindByName(string name)
        {
            return _catsContext.Cats.FirstOrDefaultAsync(x => x.Name == name);
        }

        public Task<List<Cat>> FindAllCatsByName(string name)
        {
            return _catsContext.Cats.Where(x => x.Name==name).ToListAsync();
        }
    }
}
