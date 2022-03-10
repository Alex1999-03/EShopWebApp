using EShopWebApp.Data;
using EShopWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShopWebApp.Services.Interfaces
{
    public interface IBrandServices
    {
        public Task<bool> IsExists(int? id);
        public Task Create(BrandViewModel viewModel);
        public Task Delete(Brand brand);
        public Task Edit(Brand brand, BrandViewModel viewModel);
        public Task<Brand?> Get(int? id);
        public Task<BrandViewModel?> GetViewModel(int? id);
        public Task<List<BrandViewModel>> GetListViewModel();
        public Task<SelectList> GetSelectList();
        public Task<SelectList> GetSelectListById(int? id);
    }
}
