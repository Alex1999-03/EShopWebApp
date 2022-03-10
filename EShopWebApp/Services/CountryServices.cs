using EShopWebApp.Data;
using EShopWebApp.Models;
using EShopWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EShopWebApp.Services
{
    public class CountryServices : ICountryServices
    {
        private readonly EShopDBContext _db;
        private readonly IStateServices _stateServices;
        public CountryServices(EShopDBContext db, IStateServices stateServices)
        {
            _db = db;
            _stateServices = stateServices;
        }

        public async Task<bool> IsExists(int? id)
        {
            return await _db.Countries.AnyAsync(x => x.Id == id);
        }

        public async Task Create(CountryViewModel viewModel)
        {
            var country = new Country { Name = viewModel.Name };
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Country country)
        {
            await _stateServices.DeleteAllByCountryId(country.Id);
            _db.Countries.Remove(country);
            await _db.SaveChangesAsync();
        }

        public async Task Edit(Country country, CountryViewModel viewModel)
        {
            country.Name = viewModel.Name;
            _db.Countries.Update(country);
            await _db.SaveChangesAsync();
        }

        public async Task<Country?> Get(int? id)
        {
            return await _db.Countries.FindAsync(id);
        }

        public async Task<CountryViewModel?> GetViewModel(int? id)
        {
            return await _db.Countries.Where(x => x.Id == id).Select(x => new CountryViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).FirstOrDefaultAsync();
        }

        public async Task<List<CountryViewModel>> GetListViewModel()
        {
            return await _db.Countries.Select(x => new CountryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }
    }
}
