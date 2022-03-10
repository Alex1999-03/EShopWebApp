using EShopWebApp.Data;
using EShopWebApp.Models;
using EShopWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EShopWebApp.Services
{
    public class ProductCartServices : IProductCartServices
    {
        private readonly EShopDBContext _db;

        public ProductCartServices(EShopDBContext db)
        {
            _db = db;
        }

        public async Task Create(int cartId, Product product)
        {
            var productCart = new ProductCart
            {
                CartId = cartId,
                ProductId = product.Id,
                UnitPrice = product.Price,
                Units = 1,
            };
            _db.ProductCarts.Add(productCart);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(ProductCart productCart)
        {
            _db.ProductCarts.Remove(productCart);
            await _db.SaveChangesAsync();
        }

        public async Task<List<ProductCartViewModel>> GetViewModelsByCartId(int CartId)
        {
            return await _db.ProductCarts.Where(x => x.CartId == CartId).Select(x => new ProductCartViewModel
            {
                Id = x.Id,
                CartId = x.CartId,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                UnitPrice = x.UnitPrice,
                Units = x.Units
            }).ToListAsync();
        }

        public async Task<ProductCart?> GetByCartAndProduct(int cartId, int productId)
        {
            return await _db.ProductCarts.Where(x => x.CartId == cartId && x.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task IncrementUnits(ProductCart productCart)
        {
            productCart.Units++;
            _db.ProductCarts.Update(productCart);
            await _db.SaveChangesAsync();
        }

        public async Task<ProductCart?> Get(int id)
        {
            return await _db.ProductCarts.FindAsync(id);
        }

        public async Task<List<ProductCart>> GetAllByCartId(int cartId)
        {
            return await _db.ProductCarts.Where(x => x.CartId == cartId).ToListAsync();
        }
    }
}
