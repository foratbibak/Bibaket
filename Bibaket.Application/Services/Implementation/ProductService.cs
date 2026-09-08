using Bibaket.Application.Generator;
using Bibaket.Application.Mapper;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Contracts;
using Bibaket.Domain.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productrepository;

        public ProductService(IProductRepository productrepository)
        {
            this._productrepository = productrepository;
        }
        public async Task CreateProductAsync(AdminCreateProductViewModel model)
        {
            var imageName = await SaveImageFileAsync(model.ImageFile);

            var product=ProductMapper.MapToProduct(model,imageName);

            _productrepository.Add(product);
            _productrepository.Save();
        }

        public Task EditProductAsync(AdminCreateProductViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminFilterProductViewModel> ProductFilterAsync(AdminFilterProductViewModel model)
        {
            var query = await _productrepository.ProductFilterAsync();
            #region Filter
            query = query.Where(p => !p.IsDelete);
            if (!string.IsNullOrEmpty(model.Title))
            {
                query = query.Where(c => c.Title.Contains(model.Title));
            }
            #endregion

            #region Sorting
            query = query.OrderByDescending(p => p.CreatDate);
            #endregion


            #region Paging
            await model.Paging(ProductMapper.MapToProductViewModel(query));

            #endregion

            return model;

        }


        #region Utilites
        private async Task<string> SaveImageFileAsync(IFormFile file)
        {
            if (file == null) return "NoPhoto.jpg";
            var AvatarName = NameGenerator.GenerateUniqName() +
                Path.GetExtension(file.FileName);

            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages", AvatarName);
            using (var stream = System.IO.File.Create(savePath))
            {
                await file.CopyToAsync(stream);
            }

            return AvatarName;
        }
        #endregion
    }
}
