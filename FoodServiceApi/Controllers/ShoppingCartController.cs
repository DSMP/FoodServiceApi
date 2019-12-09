using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodServiceApi.Model.Db;
using FoodServiceApi.Model.Dtos;
using FoodServiceApi.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        ShoppingCartSingleton _shoppingCartSingleton;
        FoodServiceContext _context;
        public ShoppingCartController(FoodServiceContext context, ShoppingCartSingleton shoppingCartSingleton)
        {
            _context = context;
            _shoppingCartSingleton = shoppingCartSingleton;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            ShoppingCart shoppingCart = GetUserShoppingCart();
            if (shoppingCart.AddProductToBasket(productDto))
            {
                return Ok("added");
            }
            else
            {
                return Ok("outofstock");
            }
        }

        [Authorize]
        [HttpPost("remove")]
        public IActionResult RemoveProduct([FromBody]ProductDto productDto)
        {
            ShoppingCart shoppingCart = GetUserShoppingCart();
            shoppingCart.RemoveProducts(productDto);
            return Ok("removed");
        }

        [Authorize]
        [HttpGet("total")]
        public decimal GetTotalPrice()
        {
            ShoppingCart shoppingCart = GetUserShoppingCart();
            return shoppingCart.TotalCost;
        }

        [Authorize]
        [HttpGet("discounted")]
        public decimal GetDiscountedPrice()
        {
            ShoppingCart shoppingCart = GetUserShoppingCart();
            return shoppingCart.DiscountedCost;
        }
        /// <summary>
        /// true - applied, voucher is valid
        /// false - not applied, voucher is invalid
        /// </summary>
        /// <param name="voucher"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("apply-voucher")]
        public bool ApplyVoucher(string voucher)
        {
            ShoppingCart shoppingCart = GetUserShoppingCart();
            return shoppingCart.TryAppliedVoucher(_context, voucher);
        }

        private ShoppingCart GetUserShoppingCart()
        {
            var shoppingCart = _shoppingCartSingleton.ShoppingCarts[User.Identity.Name];
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
                _shoppingCartSingleton.ShoppingCarts[User.Identity.Name] = shoppingCart;
            }

            return shoppingCart;
        }
    }
}