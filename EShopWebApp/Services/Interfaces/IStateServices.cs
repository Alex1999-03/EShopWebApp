using EShopWebApp.Data;
using EShopWebApp.Models;

namespace EShopWebApp.Services.Interfaces
{
    public interface IStateServices
    {
        public Task<bool> IsExists(int? id);
        public Task Create(StateViewModel viewModel);
        public Task Delete(State state);
        public Task DeleteAllByCountryId(int? countryId);
        public Task Edit(State state, StateViewModel viewModel);
        public Task<State?> Get(int? id);
        public Task<List<State>> GetByCountryId(int? countryId);
        public Task<StateViewModel?> GetViewModel(int? id);
        public Task<List<StateViewModel>> GetListViewModelByCountryId(int? id);
        public Task<List<StateViewModel>> GetListViewModel();
    }
}
