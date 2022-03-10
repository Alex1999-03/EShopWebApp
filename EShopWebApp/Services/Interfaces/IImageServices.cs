using EShopWebApp.Data;

namespace EShopWebApp.Services.Interfaces
{
    public interface IImageServices
    {
        public Task Create(IFormFile image, int productId);
        public Task Delete(Image image);
        public Task DeleteAllByProductId(int productId);
    }
}
