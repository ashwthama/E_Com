using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Models.Sales
{
    public class SalesDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesDetailId { get; set; }
        public string InvoiceID { get; set; }
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public int SaleQty { get; set; }
        public int NewStockQty { get; set; }
        public float SellingPrice { get; set; }

    }
}
