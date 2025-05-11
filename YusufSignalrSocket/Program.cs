using MongoDB.Driver;
using YusufSignalrSocket.Hubs;
using YusufSignalrSocket.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new MongoClient(config["MongoDbSettings:ConnectionString"]);
});

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddHostedService<ProductWatcherService>();

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapControllers();
app.MapRazorPages();
app.MapHub<PriceHub>("/priceHub");

app.Run();
