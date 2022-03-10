using EShopWebApp.Data;
using EShopWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EShopWebApp.Services
{
    public class CartServices : ICartServices
    {
        private readonly EShopDBContext _db;
        private readonly IProductServices _productServices;
        public CartServices(EShopDBContext db, 
            IProductServices productServices)
        {
            _db = db;
            _productServices = productServices;
        }

        public async Task<Cart> Create(string buyerId)
        {
            var cart = new Cart { BuyerId = buyerId };
            _db.Carts.Add(cart);
            await _db.SaveChangesAsync();
            return cart;
        }

        public async Task Delete(Cart cart)
        {
            _db.Carts.Remove(cart);
            await _db.SaveChangesAsync();
        }

        public async Task<Cart?> GetByBuyerId(string buyerId)
        {
            return await _db.Carts.Where(x => x.BuyerId == buyerId)
                .Include(x => x.ProductCarts)
                .FirstOrDefaultAsync();
        }
    }
}
