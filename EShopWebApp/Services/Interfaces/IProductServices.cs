using EShopWebApp.Data;
using EShopWebApp.Models;

namespace EShopWebApp.Services.Interfaces
{
    public interface IProductServices
    {
        public Task<bool> IsExists(int? id);
        public Task Create(ProductViewModel viewModel);
        public Task Delete(Product product);
        public Task Edit(Product product, ProductEditViewModel viewModel);
        public Task<Product?> Get(int? id);
        public Task<ProductViewModel?> GetViewModel(int? id);
        public Task<ProductEditViewModel?> GetEditViewModel(int? id);
        public Task<List<ProductViewModel>> GetListViewModel();
        public Task DecrementUnits(Product product, int quantity);
    }
}
