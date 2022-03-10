using EShopWebApp.Data;
using EShopWebApp.Services.Interfaces;

namespace EShopWebApp.Services
{
    public class OrderDetailServices : IOrderDetailServices
    {
        private readonly EShopDBContext _db;

        public OrderDetailServices(EShopDBContext db)
        {
            _db = db;
        }

        public async Task Create(int orderId, ProductCart productCart)
        {
            var orderDetail = new OrderDetail
            {
                OrderId = orderId,
                ProductId = productCart.ProductId,
                UnitPrice = productCart.UnitPrice,
                Units = productCart.Units,
            };
            _db.OrderDetails.Add(orderDetail);
            await _db.SaveChangesAsync();
        }
    }
}
