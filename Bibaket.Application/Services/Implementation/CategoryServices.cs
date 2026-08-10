using Bibaket.Application.Generator;
using Bibaket.Application.Mapper;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.ViewModels.Categories;
using Bibaket.Domin.Models.Users;
using Bibaket.Ifra.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Implementation
{
    public class CategoryServices(ICategoryRepository categoryRepository, IMemoryCache cache) : ICategoryServices
    {
        public async Task CreateCategoryAsync(AdminCreateCategoryViewModel category)
        {

            #region Save Avatar
            var Img = await SaveImageFileAsync(category.ImageFile);
            category.ImageName = Img;
            #endregion

            Category categoryMap = CategoryMapper.MapToCategory(category);

            await categoryRepository.CreateCategoryAsync(categoryMap);
            await categoryRepository.SaveChangeAsync();

        }
        #region Utilites
        private async Task<string> SaveImageFileAsync(IFormFile file)
        {
            if (file == null) return "NoPhoto.jpg";
            var imageName = NameGenerator.GenerateUniqName() +
                Path.GetExtension(file.FileName);

            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CategoryImages", imageName);
            using (var stream = System.IO.File.Create(savePath))
            {
                await file.CopyToAsync(stream);
            }

            return imageName;
        }
        #endregion
        public Task EditCategoryAsync(AdminCreateCategoryViewModel category)
        {
            throw new NotImplementedException();
        }

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

        public async Task<bool> IsExistSlug(string slug)
        {
            return await categoryRepository.IsExistSlug(slug);
        }
    }
}
