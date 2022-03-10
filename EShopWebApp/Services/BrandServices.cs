using EShopWebApp.Data;
using EShopWebApp.Models;
using EShopWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EShopWebApp.Services
{
    public class BrandServices : IBrandServices
    {
        private readonly EShopDBContext _db;

        public BrandServices(EShopDBContext db)
        {
            _db = db;
        }

        public async Task<bool> IsExists(int? id)
        {
            return await _db.Brands.AnyAsync(x => x.Id == id);
        }

        public async Task Create(BrandViewModel viewModel)
        {
            var brand = new Brand { Name = viewModel.Name };
            _db.Brands.Add(brand);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Brand brand)
        {
            _db.Remove(brand);
            await _db.SaveChangesAsync();
        }

        public async Task Edit(Brand brand, BrandViewModel viewModel)
        {
            brand.Name = viewModel.Name;
            _db.Brands.Update(brand);
            await _db.SaveChangesAsync();
        }

        public async Task<Brand?> Get(int? id)
        {
            return await _db.Brands.FindAsync(id);
        }

        public async Task<BrandViewModel?> GetViewModel(int? id)
        {
            return await _db.Brands.Where(x => x.Id == id).Select(x => new BrandViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefaultAsync();
        }

        public async Task<List<BrandViewModel>> GetListViewModel()
        {
            return await _db.Brands.Select(x => new BrandViewModel
            {
                Id = x.Id,
                Name= x.Name
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
