using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EShopWebApp.Data;
using EShopWebApp.Services.Interfaces;
using EShopWebApp.Utils.Constants;
using Microsoft.EntityFrameworkCore;

namespace EShopWebApp.Services
{
    public class ImageServices : IImageServices
    {
        private readonly Cloudinary _cloudinary;
        private readonly EShopDBContext _db;

        public ImageServices(EShopDBContext db, Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
            _db = db;
        }

        private async Task<ImageUploadResult?> Upload(IFormFile image)
        {
            var result = await _cloudinary.UploadAsync(new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
                Folder = CloudinarySettings.FOLDER
            }).ConfigureAwait(false);
            return result;
        }

        public async Task Create(IFormFile image, int productId)
        {
            var result = await Upload(image);
            var newImage = new Image
            {
                ProductId = productId,
                Height = result!.Height,
                Width = result!.Width,
                PublicId = result!.PublicId,
                SecureUrl = result!.SecureUrl.AbsoluteUri
            };
            _db.Images.Add(newImage);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(Image image)
        {
            _db.Images.Remove(image);
            await _db.SaveChangesAsync();
        }

        private async Task DeleteResource(string publicId)
        {
           await _cloudinary.DeleteResourcesAsync(publicId);
        }

        public async Task DeleteAllByProductId(int productId)
        {
            var images = await GetListByProductId(productId);
            foreach (var image in images)
            {
                await DeleteResource(image.PublicId);
                await Delete(image);
            }
        }

        private async Task<List<Image>> GetListByProductId(int productId)
        {
            return await _db.Images.Where(x => x.ProductId == productId).ToListAsync();
        }
    }
}
