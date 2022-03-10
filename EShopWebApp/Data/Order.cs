using System;
using System.Collections.Generic;

namespace EShopWebApp.Data
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string BuyerId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
