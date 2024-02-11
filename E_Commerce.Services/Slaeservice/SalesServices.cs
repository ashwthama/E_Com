using E_Commerce.Domain.Models.Sales;
using E_Commerce.Repository.SaleSRepository;
using E_Commerce.Repository.UserRepo;
using E_Commerce.ViewModels.ResponseViewModel;
using E_Commerce.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Slaeservice
{
    public class SalesServices : ISalesServices
    {
        private readonly ISalesRepo _slsRepo;
        private readonly IUserRepository _usrRepo;
        
        public SalesServices(ISalesRepo slsRepo, IUserRepository usrRepo)
        {
            _slsRepo = slsRepo;
            _usrRepo = usrRepo;
        }
        //for getting the invoice detail
        public SalesMaster GetSaleMasterByUserID(int id)
        {

            var userlist = _slsRepo.GetSaleMasterByUserId(id);

            //var usersViewModel = _mapper.Map<ProductVM>(userlist);
            var usersViewModel = new SalesMaster()
            {
                SaleMasterId = id,
                InvoiceID = userlist.InvoiceID,
                UserId = userlist.UserId,
                InvoiceDate = userlist.InvoiceDate,
                SubTotal = userlist.SubTotal,
                DeliveryAddress = userlist.DeliveryAddress,
                Zipcode = userlist.Zipcode,
                State = userlist.State,
                Country = userlist.Country,
                DeliveryDate = userlist.DeliveryDate,
            };

            return usersViewModel;

        }

        public ResponseVM AddSalesMaster(SalesMaster slsMaster)
        {
            var response = new ResponseVM();
            try
            {
                   var i = _slsRepo.CountMasterData() + 1;
                
                
                    slsMaster.InvoiceID = "ORD"+i.ToString("D3") ;
                
                     

                    slsMaster.InvoiceDate = DateTime.Now;
                    var userdata = _usrRepo.GetDataByID(slsMaster.UserId);
                    slsMaster.DeliveryAddress = userdata.Address;
                    slsMaster.Zipcode = userdata.Zipcode.ToString();
                    slsMaster.State = userdata.State;
                    slsMaster.Country = userdata.Country;
                    slsMaster.DeliveryDate = DateTime.Now.AddDays(10);
                    _slsRepo.AddSaleMaster(slsMaster);


               
                    response.Message = "Add Successfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Response = slsMaster.InvoiceID;





            }
            catch (Exception ex)
            {
                response.Message = "Exception occur while adding data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;

            }
            return response;

        }


        //get SalesDetailByInvoiceid
        public List<SalesDetail> GetSaleDetailByInvoiceId(string invoiceid)
        {
            var saledetailData=_slsRepo.GetSaleDetailByInvoiceId(invoiceid);
            return saledetailData;
        }
        //get saleMaster By iNvoice id 
        public SalesMaster GetSaleMasterByInvoiceId(string invoiceid)
        {
            var saledetailData = _slsRepo.GetSaleMasterByInvoiceId(invoiceid);
            return saledetailData;
        }


        //Deleting the Sales detail on cancel order
        public ResponseVM DeleteSaleDetailByInviceID(string invoiceid)
        {
            var response = new ResponseVM();
            try
            {
                if (invoiceid == null)
                {
                    response.Message = "Id  Can't be null";
                }
                //salemasterdelete on invoice id

                var deleteSaleMaster = _slsRepo.GetSaleMasterByInvoiceId(invoiceid);

                _slsRepo.DeleteSalesMasterByInvoiceId(deleteSaleMaster);     
                  //Sale Detail Delete Part
                var data = _slsRepo.GetSaleDetailByInvoiceId(invoiceid);

                foreach(var item in data)
                {
                    _slsRepo.DeleteSalesDeatailByInvoiceId(item);
                }

               
            }
            catch (Exception ex)
            {
                response.Message = "Exception occur while deleting data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;
            }

            return response;

        }
        public ResponseVM AddSalesDetail(SaleDetailVM slsDetail)
        {
            var response = new ResponseVM();
            try 
            { 

            var SaleDetail = new SalesDetail();

            var SalesMaster = _slsRepo.GetSaleMasterByUserId(slsDetail.UserId);

            var CartDetailData = _slsRepo.GetCartDetailByMasterId(slsDetail.CartMasterId);




            foreach (var crtdetailData in CartDetailData)
            {
                SaleDetail.SalesDetailId = 0;
                SaleDetail.InvoiceID = SalesMaster.InvoiceID;
                SaleDetail.ProductID = crtdetailData.ProductId;
                var Productdetail = _slsRepo.GetProductDetailByProductId(SaleDetail.ProductID);
                SaleDetail.ProductCode = Productdetail.ProductCode;
                SaleDetail.SaleQty = crtdetailData.Quantity;
                SaleDetail.NewStockQty = Productdetail.Stock - SaleDetail.SaleQty;
                SaleDetail.SellingPrice = Productdetail.SellingPrice*crtdetailData.Quantity;
                   _slsRepo.AddSaleDetail(SaleDetail);
                }
            

            response.Message = " AddSuccesfull";
        }
           catch (Exception ex)
            {
                response.Message = "Exception occur while adding data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;

            }
            return response;
        }
    }
}
