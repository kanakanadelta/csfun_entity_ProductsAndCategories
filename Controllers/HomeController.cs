using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost("Home/products/associate/{productId}")]
        public IActionResult ProductAssociation(int productId, int categoryId)
        {
            
            Product product = dbContext
            .Products
            .FirstOrDefault(p => p.ProductId == productId);

            
            Category category = dbContext
            .Categories
            .FirstOrDefault(c => c.CategoryId == categoryId);

            Association ass = new Association(product, category, productId, categoryId);
            dbContext.Add(ass);
            dbContext.SaveChanges();

            return RedirectToAction("ShowProduct", new {productId = productId});
        }
        // // // //
        // SHOW //
        [HttpGet("Home/products/{productId}")]
        public IActionResult ShowProduct(int productId)
        {
            Product viewProduct = dbContext
            .Products
            .FirstOrDefault(p => p.ProductId == productId);
            
            ViewBag.Product = viewProduct;

            var productWithCategories = dbContext.Products
                .Include(product => product.Associations)
                .ThenInclude(ass => ass.Category)
                .FirstOrDefault(product => product.ProductId == productId);

            List<Association> associations = dbContext.Associations.Include(a => a.Category).ToList();
            List<Category> categories = dbContext.Categories.ToList();

            List<Category> noAssociations = new List<Category>();
            
            foreach(var c in categories)
            {
                if(!viewProduct.Associations.Any(a => a.Category == c))
                {
                    noAssociations.Add(c);
                }
            }

            ViewBag.Categories = categories;
            ViewBag.NoAssociations = noAssociations;
            
            return View(productWithCategories);
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
