using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodyMan.Models
{
    public class OrderItemViewModel
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int FoodItemID { get; set; }
        public string FoodItemName { get; set; }
        public string FoodItemImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
    }
}