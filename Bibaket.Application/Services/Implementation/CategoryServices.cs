using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Categories;
using Bibaket.Ifra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Implementation
{
    public class CategoryServices(ICategoryRepository categoryRepository) : ICategoryServices
    {
        public async Task<IEnumerable<Category>> GetAllCategoryForMegaMenu()
        {
            return await categoryRepository.GetAllCategory();
        }
    }
}
