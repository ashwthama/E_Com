using E_Commerce.Domain.Models;
using E_Commerce.Domain.Models.Cart;
using E_Commerce.Domain.Models.Sales;

namespace E_Commerce.Repository.SaleSRepository
{
    public interface ISalesRepo
    {
        void AddSaleMaster(SalesMaster slmstr);
        void AddSaleDetail(SalesDetail sldetail);
        int CountMasterData();
        SalesMaster GetSaleMasterByUserId(int userid);
        //cartDetail
        List<CartDetail> GetCartDetailByMasterId(int masterid);
        Productt GetProductDetailByProductId(int productid);
        List<SalesDetail> GetSaleDetailByInvoiceId(string invoiceid);
        void DeleteSalesDeatailByInvoiceId(SalesDetail data);

        void DeleteSalesMasterByInvoiceId(SalesMaster data);
        SalesMaster GetSaleMasterByInvoiceId(string invoiceid);
    }
}