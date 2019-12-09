using FoodServiceApi.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServiceApi.Model.Db
{
    public class FoodServiceContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<User> Users { get; set; }
        public FoodServiceContext(DbContextOptions<FoodServiceContext> options) : base(options)
        {

        }
    }
}
