using System;
using System.Collections.Generic;

namespace EShopWebApp.Data
{
    public partial class Address
    {
        public Address()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Street { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public int StateId { get; set; }

        public virtual State State { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
