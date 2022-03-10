using EShopWebApp.Data;
using EShopWebApp.Models;

namespace EShopWebApp.Services.Interfaces
{
    public interface IAddressServices
    {
        public Task<Address> Create(AddressViewModel viewModel);
    }
}
