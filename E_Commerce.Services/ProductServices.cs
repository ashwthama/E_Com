using AutoMapper;
using E_Commerce.Domain.Models;
using E_Commerce.Repository.ProductRepository;
using E_Commerce.ViewModels.ProductViewModel;
using E_Commerce.ViewModels.ResponseViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductServices : IProductServices
    {

        private readonly IProductRepo _prdRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _iconfiguration;
        public ProductServices(IProductRepo prdRepo, IMapper mapper, IConfiguration configuration)
        {
            _prdRepo = prdRepo;
            _mapper = mapper;
            _iconfiguration = configuration;
        }


        public List<ProductVM> GetAllProduct()
        {

            var userlist = _prdRepo.GetProducts();

            var usersViewModel = _mapper.Map<List<ProductVM>>(userlist);

            return usersViewModel;

        }

        //public List<ProductVM> GetProductByid(int id)
        //{

        //    var userlist = _prdRepo.GetOnlyUserProductById(id);

        //    var usersViewModel = _mapper.Map<List<ProductVM>>(userlist);

        //    return usersViewModel;

        //}
        public ProductVM GetProduct(int id)
        {

            var userlist = _prdRepo.GetUserProductById(id);

            var usersViewModel = _mapper.Map<ProductVM>(userlist);

            return usersViewModel;

        }

        public ResponseVM AddProduct(ProductVM prdctVM)
        {
            var response = new ResponseVM();
            try
            {
                if (prdctVM.ProductCode == null || prdctVM.ProductCode == "")
                {
                    response.Message = "All detaisls is required.";
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Response = null;


                }
                else
                {
                    var product = _mapper.Map<Productt>(prdctVM);
                    _prdRepo.AddProduct(product);
                    response.Message = "Add Successfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                }


            }
            catch (Exception ex)
            {
                response.Message = "Exception occur while adding data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;

            }
            return response;

        }



        public ResponseVM UpdateDetails(int id, ProductVM upvm)
        {
            var response = new ResponseVM();
            try
            {
                if (id == null || upvm == null)
                {
                    response.Message = "Id and Dates Can't be null";
                }
                Productt olddata = _prdRepo.GetUserProductById(id);

                var newdata = _mapper.Map<ProductVM, Productt>(upvm, olddata);

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
        // for updating the stock in product
        public ResponseVM UpdateProductStock(int id, ProductStockVM upvm)
        {
            var response = new ResponseVM();
            try
            {
                if (id ==0 || upvm == null)
                {
                    response.Message = "Id and Dates Can't be null";
                }
                Productt olddata = _prdRepo.GetUserProductById(id);

                var newdata = _mapper.Map<ProductStockVM, Productt>(upvm, olddata);

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
                if (id == null)
                {
                    response.Message = "Id  Can't be null";
                }
                var data = _prdRepo.GetUserProductById(id);


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
