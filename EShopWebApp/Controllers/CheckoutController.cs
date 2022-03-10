using EShopWebApp.Models;
using EShopWebApp.Services.Interfaces;
using EShopWebApp.Utils.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShopWebApp.Controllers
{
    [Authorize(Roles = ApplicationRoles.BUYER_ROLE)]
    public class CheckoutController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAddressServices _addressServices;
        private readonly IOrderServices _orderServices;
        private readonly IOrderDetailServices _orderDetailServices;
        private readonly ICartServices _cartServices;
        private readonly IProductCartServices _productCartServices;
        private readonly IProductServices _productServices;
        private readonly ICountryServices _countryServices;
        private readonly IStateServices _stateServices;

        public CheckoutController(UserManager<IdentityUser> userManager,
            IAddressServices addressServices,
            IOrderDetailServices orderDetailServices,
            IOrderServices orderServices,
            ICartServices cartServices,
            IProductCartServices productCartServices,
            IProductServices productServices,
            ICountryServices countryServices,
            IStateServices stateServices)
        {
            _userManager = userManager;
            _addressServices = addressServices;
            _orderDetailServices = orderDetailServices;
            _orderServices = orderServices;
            _cartServices = cartServices;
            _productCartServices = productCartServices;
            _productServices = productServices;
            _countryServices = countryServices;
            _stateServices = stateServices;
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CountryId"] = await GetCountrySelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var address = await _addressServices.Create(viewModel);
                var order = await _orderServices.Create(user.Id, address.Id);
                var card = await _cartServices.GetByBuyerId(user.Id);
                var productsCart = await _productCartServices.GetAllByCartId(card!.Id);
                foreach (var productCart in productsCart)
                {
                    await _orderDetailServices.Create(order.Id, productCart);
                    await _productCartServices.Delete(productCart);
                    var product = await _productServices.Get(productCart.ProductId);
                    await _productServices.DecrementUnits(product!, productCart.Units);
                }
                await _cartServices.Delete(card);

                return RedirectToAction("Index", "Home");
            }
            ViewData["CountryId"] = await GetCountrySelectListById(viewModel.CountryId);
            ViewData["StateId"] = await GetStateSelectListByCountryAndStates(viewModel.StateId, viewModel.CountryId);
            return View(viewModel);
        }

        public async Task<JsonResult> GetStatesByCountryId(int id)
        {
            var viewModels = await _stateServices.GetListViewModelByCountryId(id);
            return Json(viewModels);
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

        private async Task<SelectList> GetStateSelectListByCountryAndStates(int? id, int? countryId)
        {
            var viewModels = await _stateServices.GetListViewModelByCountryId(countryId);
            return new SelectList(viewModels, "Id", "Name", id);
        }
    }
}
