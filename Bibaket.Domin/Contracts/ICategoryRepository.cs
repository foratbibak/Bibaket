using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.Models.Products;
using Bibaket.Domain.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Contracts
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryById(int CatId);

        Task<IEnumerable<Category>> GetAllCategory();
        Task<bool> IsExistSlug(string slug);

        Task<IQueryable<Category>> FillterAsync();
        Task<IEnumerable<Product>> GetProductsBySlug(string slug);

        Task<List<Category>> GetByParentIdAsync(int? parentId);


        Task CreateCategoryAsync(Category category);

        Task EditCategoryAsync(Category category);
        
        Task DeleteCategoryAsync(int CatId);

        Task SaveChangeAsync();

    }
}
