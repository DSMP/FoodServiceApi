using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodServiceApi.Model.Db;
using FoodServiceApi.Model.Dtos;
using FoodServiceApi.Model.Entities;

namespace FoodServiceApi.Model.Services
{
    public class ProductService : IProductService
    {
        private readonly FoodServiceContext _context;

        public ProductService(FoodServiceContext context)
        {
            _context = context;
        }
        public List<ProductDto> GetAll()
        {
            return _context.Products.Select<Product, ProductDto>(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                CategoryName = p.Category.Name,
                Price = p.Prices.LastOrDefault().Value,
                OldPrice = p.Prices.TakeLast(p.Prices.Count - 1).FirstOrDefault().Value,
                Quantity = p.Quantity
            }).ToList();
            
        }
    }
}
