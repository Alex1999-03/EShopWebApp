using System;
using System.Collections.Generic;

namespace EShopWebApp.Data
{
    public partial class ProductCart
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }

        public virtual Cart Cart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
