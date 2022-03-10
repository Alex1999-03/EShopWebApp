using EShopWebApp.Data;
using EShopWebApp.Models;

namespace EShopWebApp.Services.Interfaces
{
    public interface ICountryServices
    {
        public Task<bool> IsExists(int? id);
        public Task Create(CountryViewModel viewModel);
        public Task Delete(Country country);
        public Task Edit(Country country, CountryViewModel viewModel);
        public Task<Country?> Get(int? id);
        public Task<CountryViewModel?> GetViewModel(int? id);
        public Task<List<CountryViewModel>> GetListViewModel();
    }
}
