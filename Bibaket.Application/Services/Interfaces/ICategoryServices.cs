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

        Task<CategoryFilterViewModel> FilterAsync(CategoryFilterViewModel filter);

        
    }
}
