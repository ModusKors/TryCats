using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Finder;

namespace BusinessLogicLayer.Repository
{
    public interface ICatRepository : IRepository<Cat>, IFinder<Cat>
    {

    }
}
