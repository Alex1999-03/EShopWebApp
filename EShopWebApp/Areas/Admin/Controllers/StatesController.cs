#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShopWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using EShopWebApp.Utils.Constants;
using EShopWebApp.Services.Interfaces;

namespace EShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.ADMIN_ROLE)]
    public class StatesController : Controller
    {
        private readonly IStateServices _stateServices;
        private readonly ICountryServices _countryServices;
        public StatesController(IStateServices stateServices, ICountryServices countryServices)
        {
            _stateServices = stateServices;
            _countryServices = countryServices;
        }

        // GET: Admin/States
        public async Task<IActionResult> Index()
        {
            //var eShopDBContext = _context.States.Include(s => s.Country);
            return View(await _stateServices.GetListViewModel());
        }

        // GET: Admin/States/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CountryId"] = await GetCountrySelectList();
            return View();
        }

        // POST: Admin/States/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CountryId")] StateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _stateServices.Create(viewModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = await GetCountrySelectListById(viewModel.CountryId);
            return View(viewModel);
        }

        // GET: Admin/States/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _stateServices.GetViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = await GetCountrySelectListById(viewModel.CountryId);
            return View(viewModel);
        }

        // POST: Admin/States/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CountryId")] StateViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var state = await _stateServices.Get(id);
                    await _stateServices.Edit(state, viewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _stateServices.IsExists(viewModel.Id))
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
            ViewData["CountryId"] = GetCountrySelectListById(viewModel.CountryId);
            return View(viewModel);
        }

        // GET: Admin/States/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _stateServices.GetViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Admin/States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var state = await _stateServices.Get(id);
            await _stateServices.Delete(state);
            return RedirectToAction(nameof(Index));
        }

        private async Task<SelectList> GetCountrySelectList()
        {
            var viewModels = await _countryServices.GetListViewModel();
            return new SelectList(viewModels, "Id", "Name");
        }

        private async Task<SelectList> GetCountrySelectListById(int? id)
        {
            var viewModels = await _countryServices.GetListViewModel();
            return new SelectList(viewModels, "Id", "Name", id);
        }
    }
}
