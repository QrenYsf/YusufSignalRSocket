using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using YusufSignalrSocket.Data;
using YusufSignalrSocket.Dtos;
using YusufSignalrSocket.Hubs;

namespace YusufSignalrSocket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IHubContext<PriceHub> _hub;
        private readonly ApplicationDbContext _context;

        public ProductController(IHubContext<PriceHub> hub, ApplicationDbContext context)
        {
            _hub = hub;
            _context = context;
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdatePrice(int productId, [FromBody] UpdatePriceDto dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null) return NotFound();

            product.Price = dto.NewPrice;
            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("PriceUpdated", productId, dto.NewPrice);
            return Ok();
        }
    }
}
