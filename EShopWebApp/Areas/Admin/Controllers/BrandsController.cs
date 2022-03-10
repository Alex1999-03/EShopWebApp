#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EShopWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using EShopWebApp.Utils.Constants;
using EShopWebApp.Services.Interfaces;

namespace EShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.ADMIN_ROLE)]
    public class BrandsController : Controller
    {
        private readonly IBrandServices _brandServices;

        public BrandsController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }

        // GET: Admin/Brands
        public async Task<IActionResult> Index()
        {
            return View(await _brandServices.GetListViewModel());
        }

        // GET: Admin/Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BrandViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _brandServices.Create(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Admin/Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _brandServices.GetViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BrandViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var brand = await _brandServices.Get(viewModel.Id);
                    await _brandServices.Edit(brand, viewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _brandServices.IsExists(viewModel.Id))
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
            return View(viewModel);
        }

        // GET: Admin/Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _brandServices.GetViewModel(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _brandServices.Get(id);
            await _brandServices.Delete(brand);
            return RedirectToAction(nameof(Index));
        }
    }
}
