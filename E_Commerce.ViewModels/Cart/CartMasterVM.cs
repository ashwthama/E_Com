using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.ViewModels.Cart
{
    public class CartMasterVM
    {
        public int CartMasterId { get; set; }
        public int UserId { get; set; }
        public Boolean paymentStatus { get; set; }
    }
}
