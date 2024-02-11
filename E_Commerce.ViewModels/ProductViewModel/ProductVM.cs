using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.ViewModels.ProductViewModel
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public float SellingPrice { get; set; }
        public float PurchasePrice { get; set; }
        public DateTime SellingDate { get; set; }
        public int Stock { get; set; }
        public int discountId { get; set; }
       
    }
}
