using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.ViewModels.Categories;
using Bibaket.Ifra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Repositories
{
    public class CategoryRepository(EshopDbContext context) : ICategoryRepository
    {
        public async Task CreateCategoryAsync(Category category)
        {
            await context.AddAsync(category);
        }

        public async Task DeleteCategoryAsync(int CatId)
        {
            var category=await GetCategoryById(CatId);
            category.IsDelete= true;
            category.DeleteDate = DateTime.Now; 
            EditCategoryAsync(category);
        }

        public async Task EditCategoryAsync(Category category)
        {
             context.Categories.Update(category);
        }

        public async Task<IQueryable<Category>> FillterAsync()
        {
            return context.Categories.AsQueryable();
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int CatId)
        {
            return await context.Categories.FindAsync(CatId);
        }

        public async Task<bool> IsExistSlug(string slug)
        {
            return await context.Categories.AnyAsync(c=>c.Slug== slug.Trim());
        }

        public async Task SaveChangeAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
