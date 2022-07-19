using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class CatRepository : IRepository<Cat>
    {
        private CatsContext _catsContext;

        public CatRepository(CatsContext catsContext)
        {
            _catsContext = catsContext;
        }

        public Task<List<Cat>> GetAll()
        {
            return _catsContext.Cats.ToListAsync();
        }

        public Task<Cat> Get(int id)
        {
            return _catsContext.Cats.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Create(Cat item)
        {
            _catsContext.Cats.Add(item);
        }

        public void Update(Cat item)
        {
            _catsContext.Cats.Update(item);
        }

        public void Delete(int id)
        {
            var cat = _catsContext.Cats.Find(id);
            if (cat != null) _catsContext.Remove(cat);
        }
    }
}