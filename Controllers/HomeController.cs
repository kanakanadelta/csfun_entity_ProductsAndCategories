using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers
{
    public class HomeController : Controller
    {
        // context dependency injection
        private PADContext dbContext;

        public HomeController(PADContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            List<Product> allProducts = dbContext
            .Products
            .ToList();
            ViewBag.Products = allProducts;
            return View();
        }
        public IActionResult Categories()
        {
            List<Category> allCategories = dbContext
            .Categories
            .ToList();
            ViewBag.Categories = allCategories;
            return View();
        }

        // // // // /
        // CREATE //
        [HttpPost("createCategory")]
        public IActionResult CreateCategory(Category newCategory)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newCategory);
                dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            else{
                List<Category> allCategories = dbContext
                .Categories
                .ToList();
                ViewBag.Categories = allCategories;
                return View("Categories");
            }
        }

        [HttpPost("createProduct")]
        public IActionResult CreateProduct(Product newProduct)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            else
            {
                List<Product> allProducts = dbContext
                .Products
                .ToList();
                ViewBag.Categories = allProducts;
                return View("Products");
            }
        }

        // // // //
        // ERROR //
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
