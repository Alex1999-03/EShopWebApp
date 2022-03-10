using System;
using System.Collections.Generic;

namespace EShopWebApp.Data
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
