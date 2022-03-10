using System.ComponentModel.DataAnnotations;

namespace EShopWebApp.Models
{
    public class ProductCartViewModel
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = String.Empty;

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
        public decimal Total { get { return Units * UnitPrice; } }
    }
}
