using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodyMan.Models
{
	public class CartItemViewModel
	{
        public int CartItemID { get; set; }
        public int FoodItemID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageURL { get; set; }
        public decimal TotalPrice { get; set; }
    }
}