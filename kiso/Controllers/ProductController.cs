using kiso.Models;
using Microsoft.AspNetCore.Mvc;

namespace kiso.Controllers
{
    public class ProductController : Controller
    {
        private readonly KisoLightContext db;

        public ProductController(KisoLightContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CartId")))
            {
                string cartId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("CartId", cartId);
            }
            var dbp = db.Products.ToList();
            ViewBag.Product = dbp;
            return View();
        }
        public IActionResult Productdetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var dbpd = db.Products.FirstOrDefault(p => p.Id == id);
                ViewBag.dbpd = dbpd;
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult AddToCart(int productId, int count)
        {
            var product = db.Products.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            var cartId = HttpContext.Session.GetString("CartId");

            var cartItem = db.Carts.SingleOrDefault(c => c.CartId == cartId && c.ProductId == productId);

            bool isProductExistsInCart = (cartItem != null);

            if (isProductExistsInCart)
            {
                cartItem.Count += count;
                cartItem.TotalPrice = cartItem.Count * cartItem.Price; 
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

            return RedirectToAction("Index", "Cart");
        }



    }
}
