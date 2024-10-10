using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameLoop.Models; // Ensure you have the correct namespace
using System.Threading.Tasks;
using GameLoop.Data;

namespace GameLoop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageOrdersController : Controller
    {
        private readonly GameLoopContext _context; // Replace with your actual DbContext

        public ManageOrdersController(GameLoopContext context)
        {
            _context = context;
        }

        // GET: ManageOrders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).ToListAsync();
            return View(orders);
        }

        // POST: ManageOrders/ChangeStatus/5
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ManageOrders/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
