using kiso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace kiso.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly KisoLightContext _dbContext;

        public CheckoutController(KisoLightContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("Index")]
        public ActionResult Index()
        {
            int[] productId = TempData["ProductId"] as int[];
            int totalCartPriceInt = (int)TempData["TotalCartPrice"];
            int[] Count = TempData["Count"] as int[];

            if (productId != null && totalCartPriceInt != null && Count != null)
            {
                using (var db = new KisoLightContext())
                {
                    var productsWithCartInfo = db.Products
                    .Where(p => productId.Contains(p.Id))
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.ListImage,
                        p.Price,
                        Count = Count.Sum()
                    })
                    .ToList();
                    ViewBag.Products = productsWithCartInfo;
                }
                ViewBag.TotalCartPriceInt = totalCartPriceInt;

                return View();
            }
            else
            {
                return NotFound("404");
            }
        }

        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder(Order order, List<OrderDetail> OrderDetails)
        {
            if (ModelState.IsValid !=null)
            {
                var newOrder = new Order
                {
                    CustomerInfoFullname = order.CustomerInfoFullname,
                    CustomerInfoAddress = order.CustomerInfoAddress,
                    CustomerInfoMobile = order.CustomerInfoMobile,
                    CustomerInfoEmail = order.CustomerInfoEmail,
                    CustomerInfoBody = order.CustomerInfoBody,
                    CityId = order.CityId,
                    DistrictId = order.DistrictId,
                    OrderDetails = OrderDetails
                };

                _dbContext.Orders.Add(newOrder);

                _dbContext.SaveChanges();

             

                return RedirectToAction("OrderSuccess");
            }

            return View(order);
        }
        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
