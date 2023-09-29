using aspnet6_app.Data;
using aspnet6_app.Models;
using aspnet6_app.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet6_app.Controllers
{
    public class ProductController : Controller
    {   

        // To connect to the database within the controller
        private readonly ASPNetAppDbContext aspnetappDbContext;

        public ProductController(ASPNetAppDbContext aspnetappDbContext)
        {
            this.aspnetappDbContext = aspnetappDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await aspnetappDbContext.Products.ToListAsync();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel addProductRequest)
        {
            var product = new Product()
            {
                Name = addProductRequest.Name,
                Variant = addProductRequest.Variant,
                Qty = addProductRequest.Qty,
                Price = addProductRequest.Price,
                Description = addProductRequest.Description
            };

            await aspnetappDbContext.Products.AddAsync(product);
            await aspnetappDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
