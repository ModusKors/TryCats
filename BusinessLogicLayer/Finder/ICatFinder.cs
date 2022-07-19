using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Entity;

namespace BusinessLogicLayer.Finder
{
    public interface ICatFinder
    {
        Task<int> Count();

        Task<Cat?> FindCatById(int id);
        Task<Cat?> FindByName(string name);

        Task<List<Cat>> FindAllCatsByName(string name);
    }
}
