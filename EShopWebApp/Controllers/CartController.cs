using EShopWebApp.Services.Interfaces;
using EShopWebApp.Utils.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EShopWebApp.Controllers
{
    [Authorize(Roles = ApplicationRoles.BUYER_ROLE)]
    public class CartController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProductCartServices _productCartServices;
        private readonly ICartServices _cartServices;
        private readonly IProductServices _productServices;

        public CartController(UserManager<IdentityUser> userManager,
            ICartServices cartServices,
            IProductCartServices productCartServices, 
            IProductServices productServices)
        {
            _userManager = userManager;
            _cartServices = cartServices;
            _productCartServices = productCartServices;
            _productServices = productServices;

        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var card = await _cartServices.GetByBuyerId(user.Id);
            if (card == null)
                return View("Message");

            var products = await _productCartServices.GetViewModelsByCartId(card.Id);
            return products != null ? View(products) : View("Message");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var productCart = await _productCartServices.Get(id);
            await _productCartServices.Delete(productCart!);

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var card = await _cartServices.GetByBuyerId(user.Id);
            if(card!.ProductCarts.Count == 0)
            {
                await _cartServices.Delete(card!);
            }
            return RedirectToAction("Index", "Cart");
        }


        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var card = await _cartServices.GetByBuyerId(user.Id);
            var product = await _productServices.Get(id);
            if (card != null)
            {
                var productCart = await _productCartServices.GetByCartAndProduct(card.Id, id);
                if (productCart != null)
                {
                    var unitsToBuy = productCart.Units + 1;
                    if (IsPosibleAddToCart(product!.Stock, unitsToBuy))
                    {
                        await _productCartServices.IncrementUnits(productCart);
                    }else
                    {
                        TempData["type"] = "primary";
                        TempData["message"] = "You cannot add more of this product.";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    await _productCartServices.Create(card.Id, product!);
                }
            }
            else
            {
                card = await _cartServices.Create(user.Id);
                await _productCartServices.Create(card.Id, product!);
            }
            return RedirectToAction("Index", "Home");
        }

        public bool IsPosibleAddToCart(int productStock, int unitsToBuy)
        {
            return (productStock - unitsToBuy) >= 0; 
        }
    }
}
