using Bibaket.Application.Mapper;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.ViewModels.Categories;
using Bibaket.Ifra.Data.Context;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Implementation
{
    public class CategoryServices(ICategoryRepository categoryRepository, IMemoryCache cache) : ICategoryServices
    {
        public async Task<CategoryFilterViewModel> FilterAsync(CategoryFilterViewModel filter)
        {
            var query=await categoryRepository.FillterAsync();
            if (filter.ParentId.HasValue)
            {
                query=query.Where(c=>c.ParentId== filter.ParentId);
            }
            else
            {
                query = query.Where(c => c.ParentId == null);

            }
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => c.Title.Contains(filter.Title));
            }
            switch (filter.DeleteStatus)
            {
                case Domain.Enums.User.FilterDeleteStatus.All:
                    break;
                case Domain.Enums.User.FilterDeleteStatus.NotDeleted:
                    query = query.Where(c => !c.IsDelete);
                    break;
                case Domain.Enums.User.FilterDeleteStatus.Deleted:
                    query = query.Where(c => c.IsDelete);
                    break;
            }
            query = query.OrderByDescending(c => c.CreatDate);

            await filter.Paging(CategoryMapper.MapToCategoryViewModel(query));

            return filter;
        }

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

        public async Task<Category?> GetCategoryById(int CatId)
        {
           return await categoryRepository.GetCategoryById(CatId);
        }
    }
}
