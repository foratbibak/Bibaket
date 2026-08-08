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
    }
}
