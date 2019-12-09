using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodServiceApi.Model.Dtos;
using FoodServiceApi.Model.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public List<ProductDto> GetAllProducts()
        {
            return _productService.GetAll();
        }
        // not necesery, on front, app can check is out of stock
        [HttpGet("outofstock/{productId:int}")]
        public bool CheckIsOutOfStock(int productId)
        {
            return _productService.IsProductOutOfStock(productId);
        }

    }
}