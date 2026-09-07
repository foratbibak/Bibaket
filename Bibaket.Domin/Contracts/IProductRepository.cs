using Bibaket.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Contracts
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<bool> IsExistAsync(int productId);
    }
}
