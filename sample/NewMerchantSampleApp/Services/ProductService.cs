using NewMerchantSampleApp.Model;
using System.Collections.Generic;

namespace NewMerchantSampleApp.Services
{
    public static class ProductService
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Nokia Lumia 930",
                    Price = 1400,
                    ImageUrl = "/Assets/Nokia_Lumia_930.jpg"
                },

                new Product
                {
                    Name = "Microsoft Lumia 535",
                    Price = 500,
                    ImageUrl = "/Assets/Microsoft_Lumia_535.jpg"
                },

                new Product
                {
                    Name = "Prestigio MultiPhone 8400 DUO",
                    Price = 330,
                    ImageUrl = "/Assets/Prestigio_MultiPhone_8400_DUO.jpg"
                },

                new Product
                {
                    Name = "folia ochronna",
                    Price = 1,
                    ImageUrl = "/Assets/folia_ochronna.jpg"
                },
            };
        }
    }
}
