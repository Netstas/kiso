using kiso.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace kiso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly KisoLightContext db;

        public ProductApiController(KisoLightContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = db.Products.ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetProductDetails/{id}")]
        public IActionResult GetProductDetails(int id)
        {
            try
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddToCart")]
        public IActionResult AddToCart([FromForm] int productId, [FromForm] int count)
        {
            try
            {
                var product = db.Products.Find(productId);

                if (product == null)
                {
                    return NotFound();
                }

                string cartId = Guid.NewGuid().ToString();

                var existingCartItem = db.Carts.SingleOrDefault(c => c.ProductId == productId);
                if (existingCartItem != null)
                {
                    existingCartItem.Count += count;
                    existingCartItem.TotalPrice = existingCartItem.Price * existingCartItem.Count;
                }
                else
                {
                    var newCartItem = new Cart
                    {
                        CartId = cartId,
                        ProductId = productId,
                        Price = product.Price,
                        Count = count,
                        DateCreated = DateTime.Now,
                        TotalPrice = product.Price * count
                    };

                    db.Carts.Add(newCartItem);
                }

                var cartTotal = db.Carts.Where(c => c.CartId == cartId).Sum(c => c.TotalPrice);
                var cart = db.Carts.SingleOrDefault(c => c.CartId == cartId);
                if (cart != null)
                {
                    cart.Total = cartTotal;
                }

                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
