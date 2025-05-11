using YusufSignalrSocket.Models;

namespace YusufSignalrSocket.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task UpdatePriceAsync(string id, decimal newPrice);
        Task AddProductAsync(Product product);
    }
}
