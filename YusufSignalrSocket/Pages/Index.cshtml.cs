using Microsoft.AspNetCore.Mvc.RazorPages;
using YusufSignalrSocket.Models;
using YusufSignalrSocket.Services;
public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }
    public List<Product> Products { get; set; }
    public async Task OnGetAsync()
    {
        Products = await _productService.GetAllAsync();
    }
}