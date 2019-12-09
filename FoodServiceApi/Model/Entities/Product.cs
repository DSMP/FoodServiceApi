using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServiceApi.Model.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public List<Price> Prices { get; set; }
        public Category Category { get; set; }
        public Promotion Promotion { get; set; }
    }
}
