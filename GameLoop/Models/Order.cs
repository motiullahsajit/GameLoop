using GameLoop.Areas.Identity.Data;
using System;
using System.Collections.Generic;

namespace GameLoop.Models
{
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }



    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Link to the order
        public Order Order { get; set; } // Navigation property

        public int GameId { get; set; } // The game that was ordered
        public Game Game { get; set; } // Navigation property

        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; } // Price at the time of the order
    }
}
