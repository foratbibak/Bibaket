using Bibaket.Domain.Models.Products;
using Bibaket.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Mapper
{
    public static class ProductMapper
    {
        public static Product MapToProduct(AdminCreateProductViewModel model, string imageName)
        {
            return new Product()
            {
                CategoryId = model.CategoryId,
                Title = model.Title,
                Price = model.Price,
                Review = model.Review,
                Count = model.Count,
                DeatilReview = model.DeatilReview,
                CreatDate=DateTime.Now,
                IsActive = model.IsActive,
                ShortDescription = model.ShortDescription,
                DeleteDate=DateTime.Now,
                ImageName = imageName,
                
            };
        }
    }
}
