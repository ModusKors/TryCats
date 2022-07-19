using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CatRepository : ICatRepository
    {
        private CatsContext _catsContext;

        public CatRepository(CatsContext catsContext)
        {
            _catsContext = catsContext;
        }

        public void SeedData()
        {
            if (!_catsContext.Cats.Any())
            {
                _catsContext.Cats.Add(new Cat { Name = "Vasya" });
                _catsContext.Cats.Add(new Cat { Name = "Olya" });
                _catsContext.SaveChanges();
            }
        }

        public Task<int> Count() => _catsContext.Cats.CountAsync();

        public async Task<List<Cat>> GetAll()
        {
            return await _catsContext.Cats.ToListAsync();
        }

        public async Task<Cat> Get(int id)
        {
            return await _catsContext.Cats.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create(Cat item)
        {
            await _catsContext.Cats.AddAsync(item);
            await _catsContext.SaveChangesAsync();
        }

        public async Task Update(Cat item)
        {
            _catsContext.Cats.Update(item);
            await _catsContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var cat = Find(x => x.Id == id).FirstOrDefault();
            if (cat != null)
            {
                _catsContext.Cats.Remove(cat);
                await _catsContext.SaveChangesAsync();
            }
        }

        public IEnumerable<Cat> Find(Func<Cat, bool> predicate)
        {
            return _catsContext.Cats.Where(predicate).ToList();
        }

        public Cat FirstOrDefault(Func<Cat, bool> predicate) => Find(predicate).FirstOrDefault();
       
    }

    
}
