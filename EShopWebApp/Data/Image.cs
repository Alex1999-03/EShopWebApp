using System;
using System.Collections.Generic;

namespace EShopWebApp.Data
{
    public partial class Image
    {
        public int Id { get; set; }
        public string PublicId { get; set; } = null!;
        public int Height { get; set; }
        public int Width { get; set; }
        public string SecureUrl { get; set; } = null!;
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
