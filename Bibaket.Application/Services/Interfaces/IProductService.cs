using Bibaket.Domain.Models.Products;
using Bibaket.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<AdminFilterProductViewModel> ProductFilterAsync(AdminFilterProductViewModel model);

        Task CreateProductAsync(AdminCreateProductViewModel model);

        

        Task<AdminEditProductViewModel> GetEditProductForAdmin (int productId);

        Task<IEnumerable<ProductTag>> GetTagsAsync();   

        

    }
}
