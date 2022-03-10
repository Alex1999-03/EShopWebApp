using EShopWebApp.Data;

namespace EShopWebApp.Services.Interfaces
{
    public interface ICartServices
    {
        public Task<Cart> Create(string buyerId);
        public Task Delete(Cart cart);
        public Task<Cart?> GetByBuyerId(string buyerId);
    }
}
