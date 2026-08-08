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
        public Task<IQueryable<Category>> FillterAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return context.Categories.AsQueryable();
        }
    }
}
