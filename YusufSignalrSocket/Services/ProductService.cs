using MongoDB.Driver;
using YusufSignalrSocket.Models;

namespace YusufSignalrSocket.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
            _products = database.GetCollection<Product>("Products");
        } 
        public async Task<List<Product>> GetAllAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }      
        public async Task<Product> GetByIdAsync(string id)
        {
            return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdatePriceAsync(string id, decimal newPrice)
        {
            var update = Builders<Product>.Update.Set(p => p.Price, newPrice);
            await _products.UpdateOneAsync(p => p.Id == id, update);
        }

        public async Task AddProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }
    }
}
