using AutoMapper;
using E_Commerce.Domain.Models;
using E_Commerce.Domain.Models.Cart;
using E_Commerce.Repository.CartRepository;
using E_Commerce.Repository.DiscountRepository;
using E_Commerce.Repository.ProductRepository;
using E_Commerce.ViewModels;
using E_Commerce.ViewModels.Cart;
using E_Commerce.ViewModels.ResponseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.CartSevices
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartmasterrepo;
        private readonly IProductRepo _prdctrepo;
        private readonly IDiscountRepo _discountrepo;
        private readonly IMapper _mapper;

        public CartService(ICartRepo cartmasterrepo, IMapper mapper, IProductRepo prdctrepo, IDiscountRepo discountrepo)
        {
            _cartmasterrepo = cartmasterrepo;
            _mapper = mapper;
            _prdctrepo = prdctrepo;
            _discountrepo = discountrepo;
        }
        //CartMaster

        //public List<CartMasterVM> GetAllProduct()
        //{

        //    var userlist = _cartmasterrepo.GetCartMasterDetils();

        //    var usersViewModel = _mapper.Map<List<CartMasterVM>>(userlist);

        //    return usersViewModel;

        //}
        //public CartMasterVM GetCartMasterByID(int id)
        //{

        //    var userlist = _cartmasterrepo.GetCartMasterById(id);

        //    var usersViewModel = _mapper.Map<CartMasterVM>(userlist);

        //    return usersViewModel;

        //}
        //public CartMasterVM GetCartMasterByUserID(int id)
        //{

        //    var userlist = _cartmasterrepo.GetCartMasterByUserId(id);

        //    var usersViewModel = _mapper.Map<CartMasterVM>(userlist);

        //    return usersViewModel;

        //}
        public ResponseVM AddProduct(CartMasterVM crtmstVM)
        {
            var response = new ResponseVM();
            try
            {
                if (crtmstVM == null || crtmstVM.CartMasterId==null||crtmstVM.UserId==0)
                {
                    response.Message = "Not Found.";
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Response = null;

                }
                else
                {
                    //If payment of previous id is 0 the it should not add further data in cartmaster table
                    //var getcrtmaster = _cartmasterrepo.GetCartMasterByUserId(crtmstVM.UserId);
                    //if(getcrtmaster != null)
                    //{
                    //    response.Response = "Payment status of previous detail is not proceeded";
                    //    return response;
                    //}

                    crtmstVM.paymentStatus = false;
                    var product = _mapper.Map<CartMaster>(crtmstVM);
                    var crtmst= _cartmasterrepo.AddCartMaster(product);
                    response.Message = "Add Successfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Response = crtmst;
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

        public ResponseVM UpdateCartMaster(int id, PaymentVM upvm)
        {
            var response = new ResponseVM();
            try
            {
                CartMaster olddata = _cartmasterrepo.GetCartMasterById(id);

                var newdata = _mapper.Map<PaymentVM, CartMaster>(upvm, olddata);

                _cartmasterrepo.UpdateProduct(olddata, newdata);
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

        //Cart Detail
        public List<CartDetailVM> GetAllCartDetails(int id)
        {

            var userlist = _cartmasterrepo.GetCartMasterDetils(id);
            var usersViewModel = _mapper.Map<List<CartDetailVM>>(userlist);

            return usersViewModel;
        }
        //public ResponseVM AddCartDetails(CartDetailVM crtdetailVM)
        //{
        //    var response = new ResponseVM();
        //    try
        //    {
        //        if (crtdetailVM == null)
        //        {
        //            response.Message = "Not Found.";
        //            response.StatusCode = (int)HttpStatusCode.NotFound;
        //            response.Response = null;


        //        }
        //        else
        //        {
                    
        //            var product = _mapper.Map<CartDetail>(crtdetailVM);
        //            _cartmasterrepo.AddCartDetail(product);
        //            response.Message = "Add Successfully";
        //            response.StatusCode = (int)HttpStatusCode.OK;
                   
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = "Exception occur while adding data";
        //        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        response.Response = null;

        //    }
        //    return response;

        //}
        //testing
        public ResponseVM AddCartDetails(CartDetailVM crtdetailVM)
        {
            var response = new ResponseVM();
            try
            {
                if (crtdetailVM == null)
                {
                    response.Message = "Not Found.";
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Response = null;


                }
                else
                {
                    var getcartdetailByMaster_Product = _cartmasterrepo.GetCartDetailByMaster_ProductId(crtdetailVM.CartMasterId, crtdetailVM.ProductId);
                    if (getcartdetailByMaster_Product!=null)
                    {
                        response.Response = false;
                        return response;
                    }
                    else
                    {
                        var getbyproductid = _prdctrepo.GetUserProductById(crtdetailVM.ProductId);

                        var getbyDiscountId = _discountrepo.GetUserDiscountById(getbyproductid.discountId);

                        crtdetailVM.DiscountRate = getbyDiscountId.DiscountRate;

                        var product = _mapper.Map<CartDetail>(crtdetailVM);
                        _cartmasterrepo.AddCartDetail(product);
                        response.Message = "Add Successfully";
                        response.StatusCode = (int)HttpStatusCode.OK;
                    }   
                    

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

        public ResponseVM ValidateCardetil(CardVM crdvm)
        {
            var response = new ResponseVM();
            try
            {

                    var obj = _cartmasterrepo.ValidateCard(crdvm.CardNumber, crdvm.ExpiryDate, crdvm.CVV);
                if (obj != null)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Payment succesfully";
                    response.Response = true;
                    return response;
                }


                }

            
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "something went wrong while login";

                response.Response = null;
            }
            return response;




        }

        public ResponseVM DeleteProduct(int masterid,int productid)
        {
            var response = new ResponseVM();
            try
            {
                if (productid == null && masterid == null)
                {
                    response.Message = "Id  Can't be null";
                }
                //var data = _cartmasterrepo.GetCartDetailById(productid);


                _cartmasterrepo.DeleteProduct(masterid, productid);
                 response.StatusCode=(int)HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occur while deleting data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;
            }

            return response;

        }

        public ResponseVM UpdateCartDetailQuant(int id, CDetailQuantVm upvm)
        {
            var response = new ResponseVM();
            try
            {
                CartDetail olddata = _cartmasterrepo.GetCartDetailByDetailId(id);

                //update Product Stock
                var getByProductId = _prdctrepo.GetUserProductById(olddata.ProductId);
                if (olddata.Quantity > getByProductId.Stock)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                     response.Response="fail";
                    return response;
                }
                else
                {
                    var updateProductStock = new Productt()
                    {
                        ProductId = getByProductId.ProductId,
                        ProductName = getByProductId.ProductName,
                        ProductCode = getByProductId.ProductCode,
                        Brand = getByProductId.Brand,
                        PurchasePrice = getByProductId.PurchasePrice,
                        SellingPrice = getByProductId.SellingPrice,
                        SellingDate = getByProductId.SellingDate,
                        Category = getByProductId.Category,
                        discountId = getByProductId.discountId,
                        Stock = getByProductId.Stock - upvm.Quantity,
                    };

                    _prdctrepo.UpdateProduct(getByProductId, updateProductStock);
                    //update Cartdetail quantity
                    var newdata = _mapper.Map<CDetailQuantVm, CartDetail>(upvm, olddata);

                    _cartmasterrepo.UpdateQuantity(olddata, newdata);
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Response = newdata;
                }                
            }
            catch (Exception ex)
            {
                response.Message = "Exception occur while updating data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;
            }

            return response;

        }

    }
}
