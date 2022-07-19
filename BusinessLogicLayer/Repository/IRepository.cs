using BusinessLogicLayer.Entity;

namespace BusinessLogicLayer.Repository
{
    public interface IRepository<T> where T : class
    {
        void SeedData();
        Task<int> Count();

        Task<List<Cat>> GetAll();
        Task<T> Get(int id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
    }
}