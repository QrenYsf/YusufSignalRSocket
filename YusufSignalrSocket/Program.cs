using Microsoft.EntityFrameworkCore;
using YusufSignalrSocket.Data;
using YusufSignalrSocket.Hubs;
using YusufSignalrSocket.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();

builder.Services.AddRazorPages();

var app = builder.Build();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product { Name = "Elma", Price = 5.00m },
            new Product { Name = "Muz", Price = 7.50m }
        );
        db.SaveChanges();
    }
}
app.UseStaticFiles();
app.MapRazorPages();
app.MapHub<PriceHub>("/priceHub");

app.Run();