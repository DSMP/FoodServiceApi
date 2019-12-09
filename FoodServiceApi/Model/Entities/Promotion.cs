using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodServiceApi.Model.Entities
{
    public class Promotion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionId { get; set; }
        public bool Name { get; set; }
        public List<Product> Products { get; set; }
        public decimal Value { get; set; }
        public bool IsPercentage { get; set; }
        public bool IsForOrder { get; set; }
    }
}