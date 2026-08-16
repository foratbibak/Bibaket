using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Mapper
{
    public static class CategoryMapper
    {
        public static IQueryable<CategoryViewModel> MapToCategoryViewModel(IQueryable<Category> categories)
        {
            return categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                ParentId = c.ParentId,
                Title = c.Title,
                ImageName = c.ImageName,
                IsDeleted = c.IsDelete,
            });
        }
        public static Category MapToCategory(AdminCreateCategoryViewModel categoryViewModel)
        {
            return new Category()
            {
                Title = categoryViewModel.Title.Trim(),
                Slug = categoryViewModel.Slug.Trim(),
                ParentId = categoryViewModel.ParentId,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ImageName = categoryViewModel.ImageName
            };
        }
        public static void MapToEditCategory(Category category, AdminEditCategoryViewModel model)
        {
            category.Id = model.CategoryId;
            category.Title = model.Title;
            category.ParentId = model.ParentId;
            category.Slug = model.Slug;
            category.ImageName = model.ImageName;
            category.UpdateDate = DateTime.Now;


        }

    }
}
