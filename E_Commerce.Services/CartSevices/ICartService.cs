using E_Commerce.ViewModels;
using E_Commerce.ViewModels.Cart;
using E_Commerce.ViewModels.ResponseViewModel;

namespace E_Commerce.Services.CartSevices
{
    public interface ICartService
    {
        ResponseVM AddProduct(CartMasterVM crtmstVM);
        ResponseVM UpdateCartMaster(int id, PaymentVM upvm);
        //CartMasterVM GetCartMasterByID(int id);
        //List<CartMasterVM> GetAllProduct();
        //CartMasterVM GetCartMasterByUserID(int id);
        ResponseVM AddCartDetails(CartDetailVM crtdetailVM);
        List<CartDetailVM> GetAllCartDetails(int id);
        ResponseVM DeleteProduct(int masterid, int productid);

        ResponseVM ValidateCardetil(CardVM crdvm);
        ResponseVM UpdateCartDetailQuant(int id, CDetailQuantVm upvm);


    }
}