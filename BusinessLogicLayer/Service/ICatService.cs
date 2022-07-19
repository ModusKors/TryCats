using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public interface IService<T>
    {
        Task<List<T>> Get();
        Task<int> Count();
        public Task<T> Get(int id);
        Task<T> Get(string name);
        Task Post(T item);
        Task<bool> Put(T item);
        Task<bool> Delete(int id);
    }
}