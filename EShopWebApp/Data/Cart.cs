using System;
using System.Collections.Generic;

namespace EShopWebApp.Data
{
    public partial class Cart
    {
        public Cart()
        {
            ProductCarts = new HashSet<ProductCart>();
        }

        public int Id { get; set; }
        public string BuyerId { get; set; } = null!;

        public virtual ICollection<ProductCart> ProductCarts { get; set; }
    }
}
