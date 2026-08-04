using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Categories;
using Bibaket.Ifra.Data.Context;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Implementation
{
    public class CategoryServices(ICategoryRepository categoryRepository,IMemoryCache cache) : ICategoryServices
    {
        public async Task<IEnumerable<Category>> GetAllCategoryForMegaMenu()
        {
            string cashkey = "GetAllCategoryForMegaMenu";
            
            if(cache.TryGetValue(cashkey, out IEnumerable<Category> category))
            {
                return category;
            }
            else
            {
                category = await categoryRepository.GetAllCategory();

                cache.Set(cashkey, category,TimeSpan.FromHours(1));

                return category;
            }
        }
    }
}
