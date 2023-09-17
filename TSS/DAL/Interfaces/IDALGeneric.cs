using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDALGeneric<TEntity> where TEntity : class
    {
        public Task<List<TEntity>> Get(int id);
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<int> Add(TEntity entity);
        public Task<int> Update(TEntity entity);
        public Task<int> Remove(int id);
    }
}
