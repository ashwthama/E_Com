using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.ViewModels.Cart
{
    public class CartDetailVM
    {
        public int CartDetailId { get; set; }
        public int CartMasterId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string DiscountRate { get; set; }


    }
}
