using E_Commerce.Domain.Models.Discount;
using E_Commerce.Repository.DiscountRepository;
using E_Commerce.ViewModels.ResponseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.DiscountServices
{
    public class DIscountService: IDIscountService
    {
        private readonly IDiscountRepo _prdRepo;
       
        public DIscountService(IDiscountRepo prdRepo)
        {
            _prdRepo = prdRepo;
           
        }


        public List<Discount> GetAllProduct()
        {
            var userlist = _prdRepo.GetAllDiscountDetils();
            return userlist;
        }
        
        public Discount GetProduct(int id)
        {
           var userlist = _prdRepo.GetUserDiscountById(id);
            return userlist;
        }

        public ResponseVM AddProduct(Discount prdctVM)
        {
            var response = new ResponseVM();
            try
            {                                  
                   _prdRepo.AddProduct(prdctVM);
                   response.Message = "Add Successfully";
                   response.StatusCode = (int)HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                response.Message = "Exception occur while adding data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;

            }
            return response;

        }



        public ResponseVM UpdateDetails(int id, Discount upvm)
        {
            var response = new ResponseVM();
            try
            {
                if (id == null || upvm == null)
                {
                    response.Message = "Id and Dates Can't be null";
                }
                Discount olddata = _prdRepo.GetUserDiscountById(id);

                var newdata = new Discount()
                {
                    DiscountId =id,
                    DiscountName = upvm.DiscountName,
                    DiscountRate = upvm.DiscountRate,
                    DiscountStatus = upvm.DiscountStatus,
                };
                _prdRepo.UpdateProduct(olddata, newdata);
                response.Response = newdata;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occur while updating data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;
            }

            return response;

        }
        public ResponseVM DeleteProduct(int id)
        {
            var response = new ResponseVM();
            try
            {
                if (id == 0)
                {
                    response.Message = "Id  Can't be null";
                }
                var data = _prdRepo.GetUserDiscountById(id);
                _prdRepo.DeleteProduct(data);

            }
            catch (Exception ex)
            {
                response.Message = "Exception occur while deleting data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;
            }

            return response;

        }

    }
}
