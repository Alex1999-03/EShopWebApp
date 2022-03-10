using EShopWebApp.Data;
using EShopWebApp.Models;
using EShopWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EShopWebApp.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly EShopDBContext _db;

        public CategoryServices(EShopDBContext db)
        {
            _db = db;
        }

        public async Task<bool> IsExists(int? id)
        {
            return await _db.Categories.AnyAsync(x => x.Id == id);
        }

        public async Task Create(CategoryViewModel viewModel)
        {
            var category = new Category { Name = viewModel.Name };
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }

        public async Task Detele(Category category)
        {
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }

        public async Task Edit(Category category, CategoryViewModel viewModel)
        {
            category.Name = viewModel.Name;
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }

        public async Task<Category?> Get(int? id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<CategoryViewModel?> GetViewModel(int? id)
        {
            return await _db.Categories.Where(x => x.Id == id).Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).FirstOrDefaultAsync();
        }

        public async Task<List<CategoryViewModel>> GetListViewModel()
        {
            return await _db.Categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<SelectList> GetSelectList()
        {
            var viewModels = await GetListViewModel();
            return new SelectList(viewModels, "Id", "Name");
        }

        public async Task<SelectList> GetSelectListById(int? id)
        {
            var viewModels = await GetListViewModel();
            return new SelectList(viewModels, "Id", "Name", id);
        }
    }
}
