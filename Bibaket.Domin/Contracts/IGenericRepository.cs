using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bibaket.Domain.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        Task<List<T>> GetAllAsync();

        T GetById(object id);

        T GetByIdWithInCludes(int id);
        Task<T> GetByIdWithInCludesAsync(int id);

        Task<T>GetByIdAsync(int id);

        bool Remove(int Id);
        bool Remove(T entity);


        void Add(T entity);

        void Update(T entity);

        int Save();

        Task<int> SaveAsync();

        public T Select(Expression<Func<T,bool>> Where);

        public Task<T> SelectAsync(Expression<Func<T,bool>> Where);
    }
}
