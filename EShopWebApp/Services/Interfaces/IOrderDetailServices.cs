using EShopWebApp.Data;

namespace EShopWebApp.Services.Interfaces
{
    public interface IOrderDetailServices
    {
        public Task Create(int orderId, ProductCart productCart);
    }
}
