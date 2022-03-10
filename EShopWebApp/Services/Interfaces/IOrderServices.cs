using EShopWebApp.Data;

namespace EShopWebApp.Services.Interfaces
{
    public interface IOrderServices
    {
        public Task<Order> Create(string buyerId, int addressId);
    }
}
