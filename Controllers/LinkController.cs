using Microsoft.AspNetCore.Mvc;
using PhishGuard.Models;
using PhishGuard.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PhishGuard.Controllers
{
    public class LinkController : Controller
    {
        private readonly YourDbContext _context;
        private readonly LinkCheckerService _checker;

        public LinkController(YourDbContext context, LinkCheckerService checker)
        {
            _context = context;
            _checker = checker;
        }

        public IActionResult Index()
        {
            var links = _context.Links.OrderByDescending(l => l.CheckedDate).ToList();
            return View(links);
        }

        [HttpPost]
        public async Task<IActionResult> CheckLink(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                TempData["Error"] = "Please provide a valid URL.";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var status = await _checker.CheckLinkAsync(url);
                var link = new Link
                {
                    URL = url,
                    Status = status,
                    Source = "Internal Heuristics",
                    CheckedDate = DateTime.Now
                };

                _context.Links.Add(link);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = $"Link analyzed successfully: Status is {status}.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error scanning link: {ex.Message}";
                Console.WriteLine($"Scan error: {ex}");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int id)
        {
            var link = _context.Links.FirstOrDefault(l => l.LinkID == id);
            if (link == null) return NotFound();
            return View(link);
        }
    }
}
