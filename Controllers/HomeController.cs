using Microsoft.AspNetCore.Mvc;
using PhishGuard.Models;
using PhishGuard.Services;
using Microsoft.EntityFrameworkCore;

namespace PhishGuard.Controllers
{
    public class HomeController : Controller
    {
        private readonly YourDbContext _context;
        private readonly LinkCheckerService _checker;

        public HomeController(YourDbContext context, LinkCheckerService checker)
        {
            _context = context;
            _checker = checker;
        }

        public IActionResult Index()
        {
            var links = _context.Links.OrderByDescending(l => l.CheckedDate).ToList();
            return View("Index", links);
        }
    }
}
