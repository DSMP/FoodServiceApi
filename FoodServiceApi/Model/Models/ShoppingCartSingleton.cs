using FoodServiceApi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServiceApi.Model.Models
{
    public class ShoppingCartSingleton
    {
        public Dictionary<string, ShoppingCart> ShoppingCarts { get; set; }
    }
}
