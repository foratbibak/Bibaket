using Bibaket.Domain.Contracts;
using Bibaket.Ifra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bibaket.Ifra.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EshopDbContext _context;

        private readonly DbSet<T> _dbSet;

        public GenericRepository(EshopDbContext context)
        {
            this._context = context;
            _dbSet = _context.Set<T>();  
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public Task<List<T>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id); 
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T GetByIdWithInCludes(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdWithInCludesAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int Id)
        {
            try
            {
                T item = GetById(Id);
                _dbSet.Remove(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(T entity)
        {
            try
            {
                T item = GetById(entity);
                _dbSet.Remove(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public T Select(Expression<Func<T, bool>> Where)
        {
            return _dbSet.SingleOrDefault(Where);
        }

        public async Task<T> SelectAsync(Expression<Func<T, bool>> Where)
        {
            return await _dbSet.SingleOrDefaultAsync(Where);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
