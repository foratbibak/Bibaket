using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Products;
using Bibaket.Ifra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bibaket.Ifra.Data.Repositories
{
    public class ProductRepository(EshopDbContext context) : IProductRepository, IGenericRepository<Product>
    {
        public void Add(Product entity)
        {
            context.Products.Add(entity);
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products;
        }

        public Task<List<Product>> GetAllAsync()
        {
            return context.Products.ToListAsync();
        }

        public Product GetById(object id)
        {
            return context.Products.Find(id);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return context.Products.SingleOrDefaultAsync(p=>p.Id==id);
        }

        public Product GetByIdWithInCludes(int id)
        {
            return context.Products
                .Include(p=>p.ProductColors)
                .Include(p=>p.ProductFeatures)
                .Include(p=>p.ProductGalleries)
                .FirstOrDefault(p=>p.Id==id);
        }

        public Task<Product> GetByIdWithInCludesAsync(int id)
        {
            return context.Products
             .Include(p => p.ProductColors)
             .Include(p => p.ProductFeatures)
             .Include(p => p.ProductGalleries)
             .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<bool> IsExistAsync(int productId)
        {
            return context.Products.AnyAsync(p=>p.Id==productId);
        }

        public async Task<IQueryable<Product>> ProductFilterAsync()
        {
            return context.Products.AsQueryable();
        }

        public bool Remove(int Id)
        {
            var product=GetById(Id);
            Remove(product);
            return true;
        }

        public bool Remove(Product entity)
        {
            context.Products.Remove(entity);

            return true;
        }

        public int Save()
        {
            return context.SaveChanges();

        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public Product Select(Expression<Func<Product, bool>> Where)
        {
            return context.Products.FirstOrDefault(Where);
        }

        public Task<Product> SelectAsync(Expression<Func<Product, bool>> Where)
        {
            return context.Products.FirstOrDefaultAsync(Where);
        }

        public void Update(Product entity)
        {
            context.Products.Update(entity);
        }
    }
}
