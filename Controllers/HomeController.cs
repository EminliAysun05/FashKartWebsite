using FashKartWebsite.DataAccesLayer;
using FashKartWebsite.DataAccesLayer.Entities;
using FashKartWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml.Linq;

namespace FashKartWebsite.Controllers
{
    public class HomeController : Controller
    {
        //databasein contentini inject edirem
        private readonly AppDbContext _dbContext;



        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            var products = _dbContext.Products.ToList();
            //  HttpContext.Session.SetString()
            Response.Cookies.Append("cookie", "cookieValue", new CookieOptions { Expires = DateTimeOffset.Now.AddMinutes(5) }); ;
            var model = new HomeViewModel()
            {
                Categories = categories,
                Products = products

            };
            return View(model);
        }

        public IActionResult Basket()
        {
            //    var sessionValue = HttpContext.Session.GetString("ses");
            //    var cookieValue = Request.Cookies["cookie"];

            //    return Content(sessionValue+ "-" + cookieValue);
            var basketInString = Request.Cookies["basket"];
            var basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketInString);

            var newBasketViewModel = new List<BasketViewModel>();

            foreach (var item in basketViewModels)
            {
                var existProduct = _dbContext.Products.Find(item.ProductId);

                if (existProduct == null) continue;

                newBasketViewModel.Add(new BasketViewModel
                {
                    ProductId = existProduct.Id,
                    Name = existProduct.Name,
                    IMageUrl = existProduct.ImageUrl,
                    Price = existProduct.Price,
                    Count = item.Count,
                });
            }

            return Json(newBasketViewModel);
        }

        public async Task<IActionResult> AddToBasket(int? id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null) return BadRequest();

            var BasketViewModels = new List<BasketViewModel>();

            if (string.IsNullOrEmpty(Request.Cookies["basket"]))
            {
                BasketViewModels.Add(new BasketViewModel
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    IMageUrl = product.ImageUrl,
                    Price = product.Price,
                    Count = 1
                });


            }
            else
            {
                BasketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(Request.Cookies["basket"]);

                var existProduct = BasketViewModels.Find(x => x.ProductId == product.Id);

                if (existProduct == null)
                {
                    BasketViewModels.Add(new BasketViewModel
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        IMageUrl = product.ImageUrl,
                        Price = product.Price,
                        Count = 1
                    });
                }
                else
                {
                    existProduct.Count++;
                    existProduct.Name = product.Name;
                    existProduct.Price = product.Price;
                    existProduct.IMageUrl = product.ImageUrl;

                }
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(BasketViewModels));

            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
