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

        [HttpGet]
        public async Task<IActionResult> View(long id)
        {
            var product = await aspnetappDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product != null)
            {
                var viewModel = new ViewProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Variant = product.Variant,
                    Qty = product.Qty,
                    Price = product.Price,
                    Description = product.Description
                };
                return await Task.Run( () => View(viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            var product = await aspnetappDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product != null)
            {
                var viewModel = new ViewProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Variant = product.Variant,
                    Qty = product.Qty,
                    Price = product.Price,
                    Description = product.Description
                };
                return await Task.Run(() => View(viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ViewProductViewModel model)
        {
            var product = await aspnetappDbContext.Products.FindAsync(model.Id);

            if (product != null)
            {
                product.Name = model.Name;
                product.Variant = model.Variant;
                product.Qty = model.Qty;
                product.Price = model.Price;
                product.Description = model.Description;

                await aspnetappDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await aspnetappDbContext.Products.FindAsync(id);

            if (product != null)
            {
                aspnetappDbContext.Products.Remove(product);
                await aspnetappDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {

            var products = await aspnetappDbContext.Products
            .Where(p => p.Name.Contains(query))
            .ToListAsync();

            if (string.IsNullOrEmpty(query))
            {
                var all_products = await aspnetappDbContext.Products.ToListAsync();

                return View("Index", all_products);
            }

            return View("Index", products);
        }

    }
}
