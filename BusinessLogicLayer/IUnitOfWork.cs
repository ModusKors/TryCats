using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Finder;
using BusinessLogicLayer.Repository;

namespace BusinessLogicLayer
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
    }
}
