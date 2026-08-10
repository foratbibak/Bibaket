using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<Category?> GetCategoryById(int CatId);

        Task<IEnumerable<Category>> GetAllCategoryForMegaMenu();
        Task<bool> IsExistSlug(string slug);
        Task<CategoryFilterViewModel> FilterAsync(CategoryFilterViewModel filter);

        Task CreateCategoryAsync(AdminCreateCategoryViewModel category);
        Task EditCategoryAsync(AdminCreateCategoryViewModel category);



    }
}
