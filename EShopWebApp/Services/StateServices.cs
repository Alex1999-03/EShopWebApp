using EShopWebApp.Data;
using EShopWebApp.Models;
using EShopWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EShopWebApp.Services
{
    public class StateServices : IStateServices
    {
        private readonly EShopDBContext _db;

        public StateServices(EShopDBContext db)
        {
            _db = db;
        }

        public async Task Create(StateViewModel viewModel)
        {
            var state = new State { Name = viewModel.Name, CountryId = viewModel.CountryId };
            _db.States.Add(state);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(State state)
        {
            _db.States.Remove(state);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAllByCountryId(int? countryId)
        {
            var states = await GetByCountryId(countryId);
            foreach (var state in states)
            {
                await Delete(state);
            }
        }

        public async Task Edit(State state, StateViewModel viewModel)
        {
            state.Name = viewModel.Name;
            state.CountryId = viewModel.CountryId;
            _db.States.Update(state);
            await _db.SaveChangesAsync();
        }

        public async Task<State?> Get(int? id)
        {
            return await _db.States.FindAsync(id);
        }

        public async Task<List<State>> GetByCountryId(int? countryId)
        {
            return await _db.States.Where(x => x.CountryId == countryId).ToListAsync();
        }

        public async Task<StateViewModel?> GetViewModel(int? id)
        {
            return await _db.States.Where(x => x.Id == id).Select(x => new StateViewModel
            {
                Id = x.Id,
                CountryId = x.CountryId,
                CountryName = x.Country.Name,
                Name = x.Name
            }).FirstOrDefaultAsync();
        }

        public async Task<List<StateViewModel>> GetListViewModel()
        {
            return await _db.States.Select(x => new StateViewModel
            {
                Id = x.Id,
                CountryId = x.CountryId,
                CountryName = x.Country.Name,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<List<StateViewModel>> GetListViewModelByCountryId(int? id)
        {
            return await _db.States.Where(x => x.CountryId == id).Select(x => new StateViewModel
            {
                Id = x.Id,
                CountryId = x.CountryId,
                CountryName = x.Country.Name,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<bool> IsExists(int? id)
        {
            return await _db.States.AnyAsync(x => x.Id == id);
        }
    }
}
