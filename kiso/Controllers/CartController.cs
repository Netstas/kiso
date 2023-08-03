using kiso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;

namespace kiso.Controllers
{

    public class CartController : Controller
    {
        private readonly KisoLightContext db;

        public CartController(KisoLightContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            string cartId = HttpContext.Session.GetString("CartId");
            int count = 0;
            var dbResult = db.Carts
                .Join(db.Products,
                    cart => cart.ProductId,
                    product => product.Id,
                    (cart, product) => new
                    {
                        RecordId = cart.RecordId,
                        ProductId = cart.ProductId,
                        Name = product.Name,
                        ListImage = product.ListImage,
                        Price = product.Price,
                        Count = cart.Count,
                        TotalPrice = cart.TotalPrice
                    })
                .ToList();
            foreach (var item in dbResult)
            {
                count = item.Count;
            }

            var totalCartPrice = dbResult.Sum(item => item.TotalPrice);
            var totalCartCount = dbResult.Sum(item => item.Count);

            ViewBag.TotalCartPrice = totalCartPrice;
            ViewBag.totalCartCount = totalCartCount;
            ViewBag.dbResult = dbResult;
            ViewBag.CartId = cartId;
            ViewBag.Count = count;

            return View();
        }
        public async Task<IActionResult> DeleteCart(int id)
        {
            try
            {
                var Cart = await db.Carts.FindAsync(id);
                db.Carts.Remove(Cart);
                await db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public ActionResult Checkcart(int[] ProductId, decimal TotalCartPrice, int[] Count)
        {
            int totalCartPriceInt = Convert.ToInt32(TotalCartPrice);
            TempData["ProductId"] = ProductId;
            TempData["TotalCartPrice"] = totalCartPriceInt;
            TempData["Count"] = Count;
            string cartId = HttpContext.Session.GetString("CartId");
            var cartItems = db.Carts.Where(c => c.CartId == cartId);
            db.Carts.RemoveRange(cartItems);
            db.SaveChanges();
            return RedirectToAction("Index", "Checkout");
        }
       
    }
}