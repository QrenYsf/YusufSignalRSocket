using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Driver;
using YusufSignalrSocket.Hubs;
public class ProductWatcherService : BackgroundService
{
    private readonly IMongoCollection<BsonDocument> _collection;
    private readonly IHubContext<PriceHub> _hubContext;
    public ProductWatcherService(
        IMongoClient mongoClient,
        IHubContext<PriceHub> hubContext)
    {
        var database = mongoClient.GetDatabase("YusufSignalrSocket");
        _collection = database.GetCollection<BsonDocument>("Products");
        _hubContext = hubContext;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<BsonDocument>>()
            .Match(change =>
                change.OperationType == ChangeStreamOperationType.Update ||
                change.OperationType == ChangeStreamOperationType.Replace);

        var options = new ChangeStreamOptions
        {
            FullDocument = ChangeStreamFullDocumentOption.UpdateLookup,
            BatchSize = 10
        };
        using var cursor = await _collection.WatchAsync(pipeline, options, stoppingToken);

        await cursor.ForEachAsync(change =>
        {
            var productId = change.DocumentKey["_id"].ToString();
            decimal newPrice = change.FullDocument["Price"].AsDecimal;
            _hubContext.Clients.All.SendAsync("PriceUpdated", productId, newPrice).Wait();
        }, stoppingToken);
    }
}