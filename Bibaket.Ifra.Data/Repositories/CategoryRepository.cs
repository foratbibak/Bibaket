using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Categories;
using Bibaket.Ifra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Ifra.Data.Repositories
{
    public class CategoryRepository(EshopDbContext context) : ICategoryRepository
    {
        public async Task<IQueryable<Category>> FillterAsync()
        {
            return context.Categories.AsQueryable();
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await context.Categories.ToListAsync();
        }
    }
}
