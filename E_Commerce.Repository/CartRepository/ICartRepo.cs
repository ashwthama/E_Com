using E_Commerce.Domain.Models;
using E_Commerce.Domain.Models.Cart;

namespace E_Commerce.Repository.CartRepository
{
    public interface ICartRepo
    {

        //CartMaster GetCartMasterById(int id);
        //CartMaster GetCartMasterByUserId(int id);
        //List<CartMaster> GetCartMasterDetils();       
        CartMaster GetCartMasterById(int id);
        CartMaster AddCartMaster(CartMaster crtmast);
        void UpdateProduct(CartMaster oldp, CartMaster newp);
        CartDetail GetCartDetailById(int id);
        void AddCartDetail(CartDetail cartDetail);
        List<CartDetail> GetCartMasterDetils(int id);
        void DeleteProduct(int masterid, int productid);
        Cardd ValidateCard(string cardNum, string expiDate, int cvv);
        CartMaster GetCartMasterByUserId(int userid);

        //carddetail
        void UpdateQuantity(CartDetail oldp, CartDetail newp);
        CartDetail GetCartDetailByDetailId(int id);
        CartDetail GetCartDetailByMaster_ProductId(int masterid, int productid);
    }
}