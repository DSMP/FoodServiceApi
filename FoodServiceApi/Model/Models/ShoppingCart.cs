using FoodServiceApi.Model.Db;
using FoodServiceApi.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServiceApi.Model.Models
{
    public class ShoppingCart
    {
        private List<ProductDto> _shoopingCartProducts;
        public decimal TotalCost { get; private set; }
        public decimal DiscountedCost { get; private set; }
        public string AppliedVoucher { get; private set; }

        public void AddProductToBasket(ProductDto product)
        {
            _shoopingCartProducts.Add(product);
            TotalCost += product.Price;
        }
        public void RemoveProducts(ProductDto product)
        {
            _shoopingCartProducts.Remove(product);
            TotalCost -= product.Price;
        }
        public bool TryAppliedVoucher(FoodServiceContext context, string voucher)
        {
            var promo = context.Promotions.Where(p => p.Name.Equals(voucher)).FirstOrDefault();
            if (promo == null)
            {
                return false;
            }
            AppliedVoucher = voucher;
            _CalcDiscount(promo.Products, promo);
            
            return true;
        }

        private void _CalcDiscount(List<Entities.Product> p, Entities.Promotion promo)
        {
            DiscountedCost = 0;
            decimal res = 0m;
            if (promo.IsForOrder)
            {
                if (promo.IsPercentage)
                {
                    res = TotalCost * promo.Value;
                }
                else
                {
                    res = TotalCost - promo.Value;
                }
            }
            else
            {
                foreach (var product in p)
                {
                    if (promo.IsPercentage)
                    {
                        res = product.Prices.Last().Value * promo.Value;
                    }
                    else
                    {
                        res = product.Prices.Last().Value - promo.Value;
                    } 
                }
            }
            DiscountedCost = res;
        }
    }
}
