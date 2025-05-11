using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using YusufSignalrSocket.Dtos;
using YusufSignalrSocket.Hubs;
using YusufSignalrSocket.Services;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IHubContext<PriceHub> _hub;

    public ProductController(IProductService productService, IHubContext<PriceHub> hub)
    {
        _productService = productService;
        _hub = hub;
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdatePrice(string productId, [FromBody] UpdatePriceDto dto)
    {
        var product = await _productService.GetByIdAsync(productId);
        if (product == null) return NotFound();

        await _productService.UpdatePriceAsync(productId, dto.NewPrice);
        await _hub.Clients.All.SendAsync("PriceUpdated", productId, dto.NewPrice);
        return Ok();
    }
}