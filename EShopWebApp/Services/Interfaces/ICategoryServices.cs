using EShopWebApp.Data;
using EShopWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShopWebApp.Services.Interfaces
{
    public interface ICategoryServices
    {
        public Task<bool> IsExists(int? id);
        public Task Create(CategoryViewModel viewModel);
        public Task Detele(Category category);
        public Task Edit(Category category, CategoryViewModel viewModel);
        public Task<Category?> Get(int? id);
        public Task<CategoryViewModel?> GetViewModel(int? id);
        public Task<List<CategoryViewModel>> GetListViewModel();
        public Task<SelectList> GetSelectList();
        public Task<SelectList> GetSelectListById(int? id);

    }
}
