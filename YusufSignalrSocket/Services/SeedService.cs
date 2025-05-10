using YusufSignalrSocket.Models;
namespace YusufSignalrSocket.Services
{
    public class SeedService
    {
        private readonly IProductService _productService;
        public SeedService(IProductService productService)
        {
            _productService = productService;
        }
        public async Task SeedDataAsync()
        {
            var products = await _productService.GetAllAsync();
            if (!products.Any())
            {               
                await _productService.AddProductAsync(new Product { Name = "Elma", Price = 5.00m });
                await _productService.AddProductAsync(new Product { Name = "Muz", Price = 7.50m });
            }
        }
    }
}
