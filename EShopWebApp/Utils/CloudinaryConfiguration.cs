using CloudinaryDotNet;
using EShopWebApp.Utils.Constants;

namespace EShopWebApp.Utils
{
    public static class CloudinaryConfiguration
    {
        public static Cloudinary GetInstance(WebApplicationBuilder? builder)
        {
            var cloudName = builder?.Configuration.GetValue<string>(CloudinarySettings.CLOUDNAME);
            var apiKey = builder?.Configuration.GetValue<string>(CloudinarySettings.APIKEY);
            var apiSecret = builder?.Configuration.GetValue<string>(CloudinarySettings.APISECRET);
            return new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        }
    }
}
