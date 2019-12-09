using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServiceApi.Model.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public List<Price> Prices { get; set; }
        public Category Category { get; set; }
        public Promotion Promotion { get; set; }
    }
}
