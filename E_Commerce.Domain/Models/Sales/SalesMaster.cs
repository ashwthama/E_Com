using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Models.Sales
{
    public class SalesMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleMasterId { get; set; }
        public string InvoiceID { get; set; }
        public int UserId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int SubTotal { get; set; }
        public string DeliveryAddress { get; set; }
        public string Zipcode { get; set; }
        public string State { get; set; }
        public string Country{ get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
