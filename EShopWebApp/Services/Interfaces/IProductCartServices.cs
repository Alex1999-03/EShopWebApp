using EShopWebApp.Data;
using EShopWebApp.Models;

namespace EShopWebApp.Services.Interfaces
{
    public interface IProductCartServices
    {
        public Task Create(int cartId, Product product);
        public Task Delete(ProductCart productCart);
        public Task<ProductCart?> Get(int id);
        public Task<List<ProductCart>> GetAllByCartId(int cartId);
        public Task<List<ProductCartViewModel>> GetViewModelsByCartId(int cartId);
        public Task<ProductCart?> GetByCartAndProduct(int cartId, int productId);
        public Task IncrementUnits(ProductCart productCart);
    }
}
