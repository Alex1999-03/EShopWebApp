using EShopWebApp.Data;
using EShopWebApp.Models;
using EShopWebApp.Services.Interfaces;

namespace EShopWebApp.Services
{
    public class AddressService : IAddressServices
    {
        private readonly EShopDBContext _db;
        public AddressService(EShopDBContext db)
        {
            _db = db;
        }

        public async Task<Address> Create(AddressViewModel viewModel)
        {
            var address = new Address
            {
                StateId = viewModel.StateId,
                Street = viewModel.Street,
                Zipcode = viewModel.Zipcode,
            };
            _db.Addresses.Add(address);
            await _db.SaveChangesAsync();
            return address;
        }
    }
}
