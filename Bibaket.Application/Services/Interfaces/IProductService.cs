using Bibaket.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(AdminCreateProductViewModel model);

        Task EditProductAsync(AdminCreateProductViewModel model);

    }
}
