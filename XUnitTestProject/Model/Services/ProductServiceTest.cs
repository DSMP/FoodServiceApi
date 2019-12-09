using FoodServiceApi.Model.Db;
using FoodServiceApi.Model.Dtos;
using FoodServiceApi.Model.Entities;
using FoodServiceApi.Model.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject.Model.Services
{
    public class ProductServiceTest
    {
        private DbContextOptionsBuilder<FoodServiceContext> _optionsBuilder;

        public ProductServiceTest()
        {
            _optionsBuilder = new DbContextOptionsBuilder<FoodServiceContext>();
            _optionsBuilder.UseInMemoryDatabase("inMemoryDb");
        }
        [Fact]
        public void GetAll()
        {
            using (var context = new FoodServiceContext(_optionsBuilder.Options))
            {
                context.Products.Add(new Product { Name = "sdsd", Quantity = 2 });
                context.SaveChanges();
                IProductService productService = new ProductService(context);
                Assert.NotEmpty(productService.GetAll());
            }
        }
    }
}
