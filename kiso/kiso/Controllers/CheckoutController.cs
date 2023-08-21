using Azure;
using kiso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace kiso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutApiController : Controller
    {
        private readonly KisoLightContext _dbContext;
        private IMemoryCache _cache;
        public CheckoutApiController(KisoLightContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }
        [HttpPost("CheckoutInfo")]
        public IActionResult CheckoutInfo([FromForm] string[] ProductId, [FromForm] decimal TotalCartPrice, [FromForm] decimal[] Count, [FromForm] string[] CartId)
        {
            try
            {
                if (ProductId != null && ProductId.Length > 0 && TotalCartPrice > 0 && Count != null && Count.Length == ProductId.Length)
                {
                    var productIds = ProductId.SelectMany(item => item.Split(',').Select(id => decimal.Parse(id)));

                    var productsWithCartInfo = _dbContext.Products
                        .Where(p => productIds.Contains(p.Id))
                        .Select(p => new
                        {
                            p.Id,
                            p.Name,
                            p.ListImage,
                            p.Price,
                            Count= Count.Sum()
                        })
                        .ToList();

                    decimal totalCartPrice = 0;
                    for (int i = 0; i < productsWithCartInfo.Count && i < Count.Length; i++)
                    {
                        totalCartPrice += (decimal)productsWithCartInfo[i].Price * Count[i];
                    }



                    var response = new
                    {
                        products = productsWithCartInfo,
                        totalCartPrice = totalCartPrice
                    };

                    _cache.Set("CheckoutData", response);

                    var CartIdArr = CartId.SelectMany(item => item.Split(',')).ToArray();
                    var cartItems = _dbContext.Carts.Where(c => CartIdArr.Contains(c.CartId));

                    _dbContext.Carts.RemoveRange(cartItems);
                    _dbContext.SaveChanges();

                    return Ok(_cache);
                }
                else
                {
                    return BadRequest("Invalid input data.");
                }
            }
            catch
            {
                return BadRequest("Invalid input data.");
            }
        }

        [HttpGet("GetCheckoutInfo")]
        public IActionResult GetCheckoutInfo()
        {
            try
            {
                if (_cache.TryGetValue("CheckoutData", out var cachedData))
                {
                    return Ok(cachedData);
                }
                else
                {
                    return NotFound("no checkout data found.");
                }
            }
            catch
            {
                return BadRequest("404");
            }
        }
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder([FromForm] string CustomerInfoEmail, [FromForm] string CustomerInfoFullname, [FromForm] string CustomerInfoMobile, [FromForm] string CustomerInfoBody, [FromForm] string CustomerInfoAddress, [FromForm] string CityId, [FromForm] string DistrictId, [FromForm] string[] OrderDetails)
        {
            var orderDetailsList = new List<OrderDetail>();
            foreach (var orderDetailInfo in OrderDetails)
            {
                var numbersOnly = orderDetailInfo.Split(',');

                if (numbersOnly.Length >= 1)
                {
                    int productid = int.Parse(numbersOnly[0]);
                    int quantity = int.Parse(numbersOnly[1]);
                    int price = int.Parse(numbersOnly[2]);

                    var orderDetail = new OrderDetail
                    {
                        ProductId = productid,
                        Quantity = quantity,
                        Price = price
                    };
                    orderDetailsList.Add(orderDetail);
                }
            }

            var newOrderEntity = new Order
            {
                CustomerInfoFullname = CustomerInfoFullname,
                CustomerInfoAddress = CustomerInfoAddress,
                CustomerInfoMobile = CustomerInfoMobile,
                CustomerInfoEmail = CustomerInfoEmail,
                CustomerInfoBody = CustomerInfoBody,
                CityId = Convert.ToInt32(CityId),
                DistrictId = Convert.ToInt32(DistrictId),
                OrderDetails = orderDetailsList
            };
            _dbContext.Orders.Add(newOrderEntity);

            _dbContext.SaveChanges();
            _cache.Remove("CheckoutData");
            return Ok("ok");
        }
    }
}
