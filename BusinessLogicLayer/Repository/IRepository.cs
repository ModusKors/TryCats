using BusinessLogicLayer.Entity;

namespace BusinessLogicLayer.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}