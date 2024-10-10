using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameLoop.Models; // Adjust based on your project's structure
using System.Linq;
using System.Threading.Tasks;
using GameLoop.Data;

namespace GameLoop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SalesReportsController : Controller
    {
        private readonly GameLoopContext _context; 

        public SalesReportsController(GameLoopContext context)
        {
            _context = context;
        }

        // GET: SalesReports
        public async Task<IActionResult> Index()
        {
            var totalSales = await _context.Orders.SumAsync(o => o.TotalPrice);
            var totalOrders = await _context.Orders.CountAsync();
            var totalItemsSold = await _context.OrderItems.SumAsync(oi => oi.Quantity);
            var salesData = new SalesReportViewModel
            {
                TotalSales = totalSales,
                TotalOrders = totalOrders,
                TotalItemsSold = totalItemsSold
            };

            return View(salesData);
        }
    }

    public class SalesReportViewModel
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public int TotalItemsSold { get; set; }
    }
}
