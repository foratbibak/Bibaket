using Bibaket.Domain.Models.Products;
using Bibaket.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Mapper
{
    public static class ProductMapper
    {
        public static IQueryable<ProductViewModel> MapToProductViewModel(IQueryable<Product> query)
        {
            return query.Select(p => new ProductViewModel
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                CategoryName=p.Category.Title,
                Title = p.Title,
                Price = p.Price,
                ShortDescription= p.ShortDescription,
                Review=p.Review,
                DeatilReview=p.DeatilReview,
                ImageName=p.ImageName,
                IsActive=p.IsActive,
                IsDelete=p.IsDelete,
                CreateDate=p.CreatDate

            });
        }

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
