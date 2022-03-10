using System;
using System.Collections.Generic;

namespace EShopWebApp.Data
{
    public partial class Product
    {
        public Product()
        {
            Images = new HashSet<Image>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductCarts = new HashSet<ProductCart>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductCart> ProductCarts { get; set; }
    }
}
