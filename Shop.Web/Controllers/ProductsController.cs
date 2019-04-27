namespace Shop.Web.Controllers
{
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Shop.Web.Models;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

     [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IUserHelper userHelper;

        public ProductsController(IProductRepository productRepository, IUserHelper userHelper)
        {
            this.productRepository = productRepository;
            this.userHelper = userHelper;
        }

       
        // GET: Products
        public IActionResult Index()
        {
            return View(this.productRepository.GetAll().OrderBy(p => p.Name));

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productView)
        {
            if (ModelState.IsValid)
            {

                var path = string.Empty;

                if (productView.ImageFile != null && productView.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Products", file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await productView.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Products/{file}";
                }

                var product = ToProduct(productView, path);
                product.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await this.productRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            return View(productView);
        }

        private Product ToProduct(ProductViewModel v, string path)
        {
            return new Product
            {
                Id = v.Id,
                Name = v.Name,
                Price = v.Price,
                ImageUrl = path,
                LastPurchase = v.LastPurchase,
                LastSale = v.LastSale,
                IsAvailabe = v.IsAvailabe,
                Stock = v.Stock,
                User = v.User
            };
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            var productView = ToProductView(product);

            return View(productView);
        }

        private ProductViewModel ToProductView(Product v)
        {
            return new ProductViewModel
            {
                Id = v.Id,
                Name = v.Name,
                Price = v.Price,
                ImageUrl = v.ImageUrl,
                LastPurchase = v.LastPurchase,
                LastSale = v.LastSale,
                IsAvailabe = v.IsAvailabe,
                Stock = v.Stock,
                User = v.User
            };
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel productView)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var path = productView.ImageUrl;

                    if (productView.ImageFile != null && productView.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Products", file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await productView.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Products/{file}";
                    }

                    var product = ToProduct(productView, path);

                    product.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await this.productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.productRepository.ExistAsync(productView.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(productView);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.productRepository.GetByIdAsync(id);
            await this.productRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
