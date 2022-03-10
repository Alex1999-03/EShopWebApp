using EShopWebApp.Data;
using EShopWebApp.Services.Interfaces;

namespace EShopWebApp.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly EShopDBContext _db;

        public OrderServices(EShopDBContext db)
        {
            _db = db;
        }

        public async Task<Order> Create(string buyerId, int addressId)
        {
            var order = new Order
            {
                BuyerId = buyerId,
                AddressId = addressId,
                OrderDate = DateTime.Now,
            };
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return order;
        }
    }
}
