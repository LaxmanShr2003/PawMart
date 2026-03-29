using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodyMan.Models
{
	public class OrderItem
	{

       

            public int OrderItemID { get; set; }
            public int OrderID { get; set; }
            public int FoodItemID { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; } // Price at the time of order
            public decimal Subtotal { get; set; }
        
    }
}