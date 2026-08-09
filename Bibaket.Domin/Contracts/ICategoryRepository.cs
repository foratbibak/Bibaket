using Bibaket.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Contracts
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryById(int CatId);

        Task<IEnumerable<Category>> GetAllCategory();
        Task<IQueryable<Category>> FillterAsync();
    }
}
