using Bibaket.Domain.Contracts;
using Bibaket.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Implementation
{
    public class OrderServices:IOrderRepository
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IGenericRepository<ProductFeature> _genericRepository;

        public OrderServices(IOrderRepository orderRepository,IGenericRepository<ProductFeature> genericRepository)
        {
            this._orderRepository = orderRepository;
            this._genericRepository = genericRepository;
        }
    }
}
