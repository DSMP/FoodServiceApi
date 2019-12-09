using FoodServiceApi.Model.Db;
using FoodServiceApi.Model.Dtos;
using FoodServiceApi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServiceApi.Model.Models
{
    public class ShoppingCart
    {
        private List<ProductDto> _shoppingCartProducts;
        public decimal TotalCost { get; private set; }
        public decimal DiscountedCost { get; private set; }
        public string AppliedVoucher { get; private set; }
        public Promotion Promo { get; private set; }
        public ShoppingCart()
        {
            _shoppingCartProducts = new List<ProductDto>();
        }

        public bool AddProductToBasket(FoodServiceContext context, ProductDto product)
        {
            if (product.Quantity <= 0)
            {
                return false;
            }
            if (!_CheckProduct(context, product))
            {
                return false;
            }
            _shoppingCartProducts.Add(product);
            TotalCost += product.Price;
            _CalcDiscount(Promo);
            return true;
        }

        public void RemoveProducts(FoodServiceContext context, ProductDto product)
        {
            if (!_CheckProduct(context, product))
            {
                return;
            }
            _shoppingCartProducts.Remove(product);
            TotalCost -= product.Price;
            _CalcDiscount(Promo);
        }
        public bool TryAppliedVoucher(FoodServiceContext context, string voucher)
        {
            var promo = context.Promotions.Where(p => p.Name.Equals(voucher)).FirstOrDefault();
            if (promo == null)
            {
                return false;
            }
            AppliedVoucher = voucher;
            Promo = promo;
            _CalcDiscount(promo);
            
            return true;
        }

        private void _CalcDiscount(Promotion promo)
        {
            var p = promo.Products;
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

        private bool _CheckProduct(FoodServiceContext context, ProductDto product)
        {
            return context.Products.Any(p => p.ProductId == product.ProductId && p.Name.Equals(product.Name));
        }
    }
}
