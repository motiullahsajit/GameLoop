using GameLoop.Data; // Add this using directive
using GameLoop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq; // Add this using directive

namespace GameLoop.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameLoopContext _context; // Add context for accessing the database
        private readonly ILogger<HomeController> _logger;

        // Constructor accepting both context and logger
        public HomeController(GameLoopContext context, ILogger<HomeController> logger)
        {
            _context = context; // Initialize the context
            _logger = logger; // Initialize the logger
        }

        // Index action that retrieves games from the database
        public IActionResult Index()
        {
            var games = _context.Games.ToList(); // Fetch the list of games
            return View(games); // Pass the list of games to the view
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
