using kiso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace kiso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartApiController : Controller
    {
        private readonly KisoLightContext db;

        public CartApiController(KisoLightContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult GetCart()
        {
            try
            {
                //string cartId = HttpContext.Session.GetString("CartId");
                var dbResult = db.Carts
                    .Join(db.Products,
                        cart => cart.ProductId,
                        product => product.Id,
                        (cart, product) => new
                        {
                            RecordId = cart.RecordId,
                            ProductId = cart.ProductId,
                            CartId = cart.CartId,
                            Name = product.Name,
                            ListImage = product.ListImage,
                            Price = product.Price,
                            Count = cart.Count,
                            TotalPrice = cart.TotalPrice
                        })
                    .ToList();

                var totalCartPrice = dbResult.Sum(item => item.TotalPrice);
                var totalCartCount = dbResult.Sum(item => item.Count);

                var result = new
                {
                    TotalCartPrice = totalCartPrice,
                    TotalCartCount = totalCartCount,
                    CartItems = dbResult,

                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCartItem/{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                var cartItem = await db.Carts.FindAsync(id);
                if (cartItem == null)
                {
                    return NotFound();
                }

                db.Carts.Remove(cartItem);
                await db.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}