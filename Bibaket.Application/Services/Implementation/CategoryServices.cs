using Bibaket.Application.Generator;
using Bibaket.Application.Mapper;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Application.Utilities;
using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.ViewModels.Categories;
using Bibaket.Domin.Models.Users;
using Bibaket.Ifra.Data.Context;
using Bibaket.Ifra.Data.Repositories;
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

            #region Create Category
            Category categoryMap = CategoryMapper.MapToCategory(category);

            await categoryRepository.CreateCategoryAsync(categoryMap);
            await categoryRepository.SaveChangeAsync();
            cache.Remove(CashKeyNames.GetAllCategoryForMegaMenu);
            #endregion

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

        public async Task EditCategoryAsync(AdminEditCategoryViewModel categoryEdit)
        {
            #region MapToEdit
            var Category = await categoryRepository.GetCategoryById(categoryEdit.CategoryId);
            CategoryMapper.MapToEditCategory(Category, categoryEdit);
            #endregion

            #region Delete Img
            if (categoryEdit.ImageFile != null)
            {
                if (categoryEdit.ImageName != "NoPhoto.jpg")
                {
                    string delete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CategoryImages", categoryEdit.ImageName);
                    FileHellper.DeletePath(delete);
                }
                Category.ImageName = await SaveImageFileAsync(categoryEdit.ImageFile);
            }
            #endregion

            #region Edit Category
            await categoryRepository.EditCategoryAsync(Category);
            await categoryRepository.SaveChangeAsync();
            #endregion
        }

        public async Task<CategoryFilterViewModel> FilterAsync(CategoryFilterViewModel filter)
        {
            #region Filter Validate
            var query = await categoryRepository.FillterAsync();
            if (filter.ParentId.HasValue)
            {
                query = query.Where(c => c.ParentId == filter.ParentId);
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
            #endregion

            #region Filter
            query = query.OrderByDescending(c => c.CreatDate);

            await filter.Paging(CategoryMapper.MapToCategoryViewModel(query));

            return filter;
            #endregion
        }

        public async Task<IEnumerable<Category>> GetAllCategoryForMegaMenu()
        {
            #region Cashe
            string cashkey = CashKeyNames.GetAllCategoryForMegaMenu;

            if (cache.TryGetValue(cashkey, out IEnumerable<Category> category))
            {
                return category;
            }
            else
            {
                category = await categoryRepository.GetAllCategory();

                cache.Set(cashkey, category, TimeSpan.FromHours(1));

                return category;
            }
            #endregion
        }

        public async Task<Category?> GetCategoryById(int CatId)
        {
           return await categoryRepository.GetCategoryById(CatId);
        }

        public async Task<bool> IsExistSlug(string slug)
        {
            return await categoryRepository.IsExistSlug(slug);
        }

        public async Task DeleteCategoryAsync(int CatId)
        {
            await categoryRepository.DeleteCategoryAsync(CatId);
            await categoryRepository.SaveChangeAsync();
        }
    }
}
