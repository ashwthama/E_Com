using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Models.Cart
{
    public class CartMaster
    {
        public int CartMasterId { get; set; }
        public int UserId { get; set; }
        public Boolean paymentStatus  { get; set; }
       
        public List<CartDetail> CartDetails { get; set; }
    }
}
