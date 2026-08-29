using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<bool> IsExistAsync(int productId);
    }
}
