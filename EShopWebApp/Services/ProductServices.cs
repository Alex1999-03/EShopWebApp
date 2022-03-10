using EShopWebApp.Data;
using EShopWebApp.Models;
using EShopWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EShopWebApp.Services
{
    public class ProductServices : IProductServices
    {
        private readonly EShopDBContext _db;
        private readonly IImageServices _imageServices;

        public ProductServices(EShopDBContext db, IImageServices imageServices)
        {
            _db = db;
            _imageServices = imageServices;

        }

        public async Task Create(ProductViewModel viewModel)
        {
            var product = new Product
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Stock = viewModel.Stock,
                Price = viewModel.Price,
                BrandId = viewModel.BrandId,
                CategoryId = viewModel.CategoryId
            };
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            await CreateImages(viewModel.Images, product.Id);
        }

        private async Task CreateImages(List<IFormFile>? images, int productId)
        {
            foreach(var image in images!)
            {
                await _imageServices.Create(image, productId);
            }
        }

        public async Task Delete(Product product)
        {
            await _imageServices.DeleteAllByProductId(product.Id);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task Edit(Product product, ProductEditViewModel viewModel)
        {
            product.Description = viewModel.Description;
            product.Name = viewModel.Name;
            product.Price = viewModel.Price;
            product.Stock = viewModel.Stock;
            product.BrandId = viewModel.BrandId;
            product.CategoryId = viewModel.CategoryId;
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task<Product?> Get(int? id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<List<ProductViewModel>> GetListViewModel()
        {
            return await _db.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                UrlImage = x.Images.FirstOrDefault(y => y.ProductId == x.Id)!.SecureUrl,
                CategoryId = x.CategoryId,
                Category = x.Category.Name,
                BrandId = x.BrandId,
                Brand = x.Brand.Name
            }).ToListAsync();
        }

        public async Task<ProductViewModel?> GetViewModel(int? id)
        {
            return await _db.Products.Where(x => x.Id == id).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                UrlImage = x.Images.FirstOrDefault(y => y.ProductId == x.Id)!.SecureUrl,
                CategoryId = x.CategoryId,
                Category = x.Category.Name,
                BrandId = x.BrandId,
                Brand = x.Brand.Name
            }).FirstOrDefaultAsync();
        }
        public async Task<ProductEditViewModel?> GetEditViewModel(int? id)
        {
            return await _db.Products.Where(x => x.Id == id).Select(x => new ProductEditViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                CategoryId = x.CategoryId,
                BrandId = x.BrandId,
            }).FirstOrDefaultAsync();
        }
        public async Task<bool> IsExists(int? id)
        {
            return await _db.Products.AnyAsync(x => x.Id == id);
        }

        public async Task DecrementUnits(Product product, int quantity)
        {
            product.Stock = product.Stock - quantity;
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }
    }
}
