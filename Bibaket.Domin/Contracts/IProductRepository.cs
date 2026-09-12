using Bibaket.Domain.Models.Products;
using Bibaket.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Contracts
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<IQueryable<Product>> ProductFilterAsync();
        Task<bool> IsExistAsync(int productId);

        Task AddProductGalleryAsync(ProductGallery productGallery);
    }
}
