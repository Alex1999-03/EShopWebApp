#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EShopWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using EShopWebApp.Utils.Constants;
using EShopWebApp.Services.Interfaces;

namespace EShopWebApp.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.ADMIN_ROLE)]
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly IBrandServices _brandServices;
        private readonly ICategoryServices _categoryServices;

        public ProductsController(IProductServices productServices, 
            IBrandServices brandServices, 
            ICategoryServices categoryServices)
        {
            _productServices = productServices;
            _brandServices = brandServices;
            _categoryServices = categoryServices;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View(await _productServices.GetListViewModel());
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BrandId"] = await _brandServices.GetSelectList();
            ViewData["CategoryId"] = await _categoryServices.GetSelectList();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Stock,Price,Images,BrandId,CategoryId")] ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _productServices.Create(viewModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = await _brandServices.GetSelectListById(viewModel.BrandId);
            ViewData["CategoryId"] = await _categoryServices.GetSelectListById(viewModel.CategoryId);
            return View(viewModel);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _productServices.GetEditViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = await _brandServices.GetSelectListById(viewModel.BrandId);
            ViewData["CategoryId"] = await _categoryServices.GetSelectListById(viewModel.CategoryId);
            return View(viewModel);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Stock,Price,BrandId,CategoryId")] ProductEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _productServices.Get(id);
                    await _productServices.Edit(product, viewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _productServices.IsExists(viewModel.Id))
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
            ViewData["BrandId"] = await _brandServices.GetSelectListById(viewModel.BrandId);
            ViewData["CategoryId"] = await _categoryServices.GetSelectListById(viewModel.CategoryId);
            return View(viewModel);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _productServices.GetViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productServices.Get(id);
            await _productServices.Delete(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
