using E_Commerce.Domain.Models.Sales;
using E_Commerce.ViewModels.ResponseViewModel;
using E_Commerce.ViewModels.Sales;

namespace E_Commerce.Services.Slaeservice
{
    public interface ISalesServices
    {
        ResponseVM AddSalesMaster(SalesMaster slsMaster);
        ResponseVM AddSalesDetail(SaleDetailVM slsDetail);
        SalesMaster GetSaleMasterByUserID(int id);
        List<SalesDetail> GetSaleDetailByInvoiceId(string invoiceid);
        ResponseVM DeleteSaleDetailByInviceID(string invoiceid);

        SalesMaster GetSaleMasterByInvoiceId(string invoiceid);
    }
}