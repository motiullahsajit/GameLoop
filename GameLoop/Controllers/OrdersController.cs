using GameLoop.Data;
using GameLoop.Models; // Ensure this namespace is included
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameLoop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly GameLoopContext _context;

        public OrdersController(GameLoopContext context)
        {
            _context = context;
        }

        // GET: /Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems) // Include OrderItems
                .ThenInclude(oi => oi.Game)  // Include related Game for each OrderItem
                .ToListAsync(); // Fetch orders with related data
            return View(orders); // Return view with orders
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int gameId)
        {
            // Find the game being ordered
            var game = await _context.Games.FindAsync(gameId);
            if (game == null)
            {
                return NotFound(); // Handle game not found
            }

            // Create a new order
            var order = new Order
            {
                UserId = User.Identity.Name, // Make sure this is set correctly
                OrderDate = DateTime.Now,
                TotalPrice = game.Price, // Or calculate total for multiple items
                Status = "Pending", // Set a default status
                OrderItems = new List<OrderItem>
        {
            new OrderItem
            {
                GameId = game.Id,
                Price = game.Price,
                Quantity = 1
            }
        }
            };

            // Add the order to the context
            _context.Orders.Add(order);

            // If the context is not tracking OrderItems, explicitly add them
            foreach (var item in order.OrderItems)
            {
                _context.OrderItems.Add(item); // Ensure OrderItems are added to context
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect to the orders index or another appropriate page
            return RedirectToAction("Index", "Orders");
        }

    }
}
