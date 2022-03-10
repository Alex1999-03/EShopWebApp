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
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoryServices.GetListViewModel());
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _categoryServices.Create(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _categoryServices.GetViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = await _categoryServices.Get(id);
                    await _categoryServices.Edit(category, viewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _categoryServices.IsExists(viewModel.Id))
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

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _categoryServices.GetViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryServices.Get(id);
            await _categoryServices.Detele(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
