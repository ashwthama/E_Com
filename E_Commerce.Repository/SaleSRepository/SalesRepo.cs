using E_Commerce.Domain.Context;
using E_Commerce.Domain.Models;
using E_Commerce.Domain.Models.Cart;
using E_Commerce.Domain.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.SaleSRepository
{
    public class SalesRepo : ISalesRepo
    {
        private readonly UserContext _context;
        public SalesRepo(UserContext context)
        {
            _context = context;
        }

        public void AddSaleMaster(SalesMaster slmstr)
        {
            _context.SalesMasters.Add(slmstr);
            _context.SaveChanges();
        }

       
        public int CountMasterData()
        {
            return _context.SalesMasters.Count();
        }

        public SalesMaster GetSaleMasterByUserId(int userid)
        {
            return _context.SalesMasters.Where(x => x.UserId == userid).OrderBy(x=>x.SaleMasterId).LastOrDefault();
        }
        public void AddSaleDetail(SalesDetail sldetail)
        {
            _context.SalesDetails.Add(sldetail);
            _context.SaveChanges();
        }

        //get SalesDetailByInvoiceid
        public List<SalesDetail> GetSaleDetailByInvoiceId(string invoiceid)
        {
            return _context.SalesDetails.Where(x => x.InvoiceID == invoiceid).ToList();
        }
        //get salemaster by invoice id
        public SalesMaster GetSaleMasterByInvoiceId(string invoiceid)
        {
            return _context.SalesMasters.Where(x => x.InvoiceID == invoiceid).FirstOrDefault();
        }

        //CartDetail Table
        public List<CartDetail> GetCartDetailByMasterId(int masterid)
        {
            return _context.CartDetail.Where(x => x.CartMasterId == masterid).ToList();
        }
        //product detail
        public Productt GetProductDetailByProductId(int productid)
        {
            return _context.Product.FirstOrDefault(x => x.ProductId == productid);
        }

        //For Deleting the order in saleDetail
        public void DeleteSalesDeatailByInvoiceId(SalesDetail data)
        {
            _context.SalesDetails.Remove(data);
            _context.SaveChanges();
        }
        //For Deleting the order in saleMaster
        public void DeleteSalesMasterByInvoiceId(SalesMaster data)
        {
            _context.SalesMasters.Remove(data);
            _context.SaveChanges();
        }
    }
}
