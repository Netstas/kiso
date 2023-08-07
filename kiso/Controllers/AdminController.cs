using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kiso.Models;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace kiso.Controllers
{
    public class AdminController : Controller
    {
        private readonly KisoLightContext db;
        public AdminController(KisoLightContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminUsername") == null || HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        //ProductsCategory
        public IActionResult Category()
        {
            var dbPc = db.ProductCategories.ToList();
            ViewBag.ProductCategory = dbPc;
            return View(dbPc);
        }
        public IActionResult ProductsCategoryCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductsCategoryCreate(ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ProductCategories.Add(productCategory);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Category));
                }
                return View(productCategory);
            }
            catch
            {
                return NoContent();
            }
        }
        public IActionResult ProductsCategoryUpdate(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var dbPc = db.ProductCategories.FirstOrDefault(x => x.Id == id);
                return View(dbPc);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductsCategoryUpdate(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.Update(productCategory);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Category));
            }
            return View(productCategory);
        }
        public async Task<IActionResult> ProductsCategoryDelete(int id)
        {
            try
            {
                var Productcategories = await db.ProductCategories.FindAsync(id);
                db.ProductCategories.Remove(Productcategories);
                await db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Product
        public IActionResult Products(int page = 1)
        {
            try
            {
                int pageSize = 5;

                var dblist = db.Products
                    .Include(p => p.ProductCategory)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Wattage,
                        p.Voltage,
                        p.ColorRenderingIndex,
                        p.Guarantee,
                        p.ListImage,
                        p.Price,
                        p.PriceSale,
                        p.ProductCategory.CategoryName
                    })
                    .ToList();
                ViewBag.Products = dblist;

                int totalProducts = db.Products.Count();
                int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult ProductsCreate()
        {
            var categoryNames = db.ProductCategories
                     .ToList();

            ViewBag.CategoryNames = categoryNames;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductsCreate(Product product, List<IFormFile> ListImage)
        {
            try
            {
                if (ModelState.IsValid != null)
                {
                    product.CreateDate = DateTime.Now;
                    db.Products.Add(product);
                    await db.SaveChangesAsync();

                    var imagePaths = new List<string>();
                    foreach (var image in ListImage)
                    {
                        if (image.Length > 0)
                        {
                            var fileName = Path.GetFileName(image.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            imagePaths.Add(fileName);
                        }
                    }

                    product.ListImage = string.Join(",", imagePaths);
                    await db.SaveChangesAsync();

                    return RedirectToAction(nameof(Products));
                }
                var categoryNames = db.ProductCategories.ToList();
                ViewBag.CategoryNames = categoryNames;
                return View(product);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public IActionResult UpdateProduct(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var categoryNames = db.ProductCategories.ToList();
            ViewBag.CategoryNames = categoryNames;

            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateProduct(int Id, Product product, List<IFormFile> ListImage)
        {
            try
            {
                if (Id == null && product == null && ListImage == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid != null)
                {
                    product.CreateDate = DateTime.Now;
                    db.Products.Update(product);
                    db.SaveChanges();
                    var getProduct = db.Products.FirstOrDefault(p => p.Id == product.Id);

                    // Xóa các ảnh cũ trong thư mục
                    if (!string.IsNullOrEmpty(getProduct.ListImage) && ListImage != null && getProduct.ListImage != product.ListImage)
                    {
                        var deletedImageNames = getProduct.ListImage?.Split(',') ?? Array.Empty<string>();
                        var uploadedImageNames = product.ListImage?.Split(',') ?? Array.Empty<string>();

                        var imagesToDelete = deletedImageNames.Except(uploadedImageNames);

                        foreach (var imageName in imagesToDelete)
                        {
                            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", imageName);
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                        }
                    }
                    // Lưu ảnh mới vào thư mục
                    if (ListImage != null && ListImage.Any())
                    {
                        var imagePaths = new List<string>();
                        foreach (var image in ListImage)
                        {
                            if (image.Length > 0)
                            {
                                var fileName = Path.GetFileName(image.FileName);
                                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", fileName);

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    image.CopyTo(stream);
                                }

                                imagePaths.Add(fileName);
                            }
                        }

                        product.ListImage = string.Join(",", imagePaths);
                    }
                    db.SaveChanges();
                    return RedirectToAction(nameof(Products));

                }

                var categoryNames = db.ProductCategories.ToList();
                ViewBag.CategoryNames = categoryNames;
                return View(product);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await db.Products.FindAsync(id);
                if (product == null)
                    return NotFound();
                var imageNames = product.ListImage.Split(',');
                foreach (var imageName in imageNames)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", imageName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                db.Products.Remove(product);
                await db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Auth && Account
        public IActionResult Account()
        {
            var dbacc = db.Admins.ToList();
            ViewBag.Account = dbacc;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var admin = db.Admins.FirstOrDefault(a => a.Username == username && a.Password == password);
                if (admin != null)
                {
                    // Đăng nhập thành công, lưu thông tin admin vào session
                    HttpContext.Session.SetString("AdminUsername", admin.Username);
                    HttpContext.Session.SetInt32("AdminId", admin.Id);
                    Response.Cookies.Append("AdminUsername", admin.Username);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.ErrorMessage = "Invalid username or password.";
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            var existingAdmin = db.Admins.FirstOrDefault(a => a.Username == username);
            if (existingAdmin != null)
            {
                ViewBag.ErrorMessage = "Username already exists. Please choose a different username.";
                return View();
            }

            var newAdmin = new Admin
            {
                Username = username,
                Password = ComputeMD5Hash(password),
                Active = true
            };

            db.Admins.Add(newAdmin);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Xóa thông tin đăng nhập của admin khỏi session
            HttpContext.Session.Remove("AdminUsername");
            HttpContext.Session.Remove("AdminId");

            // Chuyển hướng người dùng đến trang đăng nhập sau khi đăng xuất
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Orders()
        {
            var orders = db.Orders.ToList();
            ViewBag.Orders = orders;
            return View();
        }
        public IActionResult CreateOrder()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            try
            {
                if (ModelState.IsValid != null)
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Orders", "Admin");
                }
                return View();
            }
            catch
            {
                return NotFound("404");
            }

        }

        private string ComputeMD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

    }

}
