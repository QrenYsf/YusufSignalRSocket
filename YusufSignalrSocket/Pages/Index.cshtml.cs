using Microsoft.AspNetCore.Mvc.RazorPages;
using YusufSignalrSocket.Data;
using YusufSignalrSocket.Models;

namespace YusufSignalrSocket.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; }

        public void OnGet()
        {
            Products = _context.Products.ToList();
        }
    }
}
