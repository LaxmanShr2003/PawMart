using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodyMan.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int FoodItemID { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
    }
}