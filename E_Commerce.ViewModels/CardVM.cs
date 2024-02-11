using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.ViewModels
{
    public  class CardVM
    {
        public int CardId { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public int CVV { get; set; }
    }
}
