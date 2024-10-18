using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public  Task CreateAsync(T entity);
        public Task DeleteAsync(int id);
        public Task<IEnumerable<T>> GetAsync();
        public Task UpdateAsync(T entity);
        public Task<T?> GetByIdAsync(int id);
    }
}
