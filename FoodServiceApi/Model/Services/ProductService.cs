using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodServiceApi.Model.Db;
using FoodServiceApi.Model.Dtos;

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
            throw new NotImplementedException();
        }
    }
}
