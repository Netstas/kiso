using kiso.Models;
using Microsoft.AspNetCore.Mvc;

namespace kiso.Controllers
{

    [ApiController]
    [Route("pay/[controller]")]
    public class ApiController : Controller
    {
        private readonly KisoLightContext _dbContext;

        public ApiController(KisoLightContext dbContext)
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

        [HttpGet("GetCities")]
        public IActionResult GetCities()
        {
            var cities = _dbContext.Cities.ToList();
            return Ok(cities);
        }

        [HttpGet("GetDistricts/{cityId}")]
        public IActionResult GetDistricts(int cityId)
        {
            var districts = _dbContext.Districts
                .Where(d => d.CityId == cityId)
                .ToList();

            return Ok(districts);
        }

    }
}
