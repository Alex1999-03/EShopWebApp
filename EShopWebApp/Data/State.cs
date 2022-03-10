using System;
using System.Collections.Generic;

namespace EShopWebApp.Data
{
    public partial class State
    {
        public State()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
