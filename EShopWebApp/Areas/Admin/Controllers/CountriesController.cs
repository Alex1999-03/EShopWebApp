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
    public class CountriesController : Controller
    {
        private readonly ICountryServices _countryServices;

        public CountriesController(ICountryServices countryServices)
        {
            _countryServices = countryServices;
        }

        // GET: Admin/Countries
        public async Task<IActionResult> Index()
        {
            return View(await _countryServices.GetListViewModel());
        }

        // GET: Admin/Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CountryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _countryServices.Create(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Admin/Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _countryServices.GetViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Admin/Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CountryViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var country = await _countryServices.Get(id);
                    await _countryServices.Edit(country, viewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _countryServices.IsExists(viewModel.Id))
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

        // GET: Admin/Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _countryServices.GetViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Admin/Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _countryServices.Get(id);
            await _countryServices.Delete(country);
            return RedirectToAction(nameof(Index));
        }
    }
}
