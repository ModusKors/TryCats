using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Finder;
using BusinessLogicLayer.Repository;

namespace BusinessLogicLayer
{
    public interface IUnitOfWork
    {
        IRepository<Cat> Cats { get; }
        ICatFinder CatsFinder { get; }
        Task<int> Commit();
    }
}
