using E_Commerce.Domain.Context;
using E_Commerce.Domain.Models;
using E_Commerce.Domain.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.CartRepository
{
    public class CartRepo : ICartRepo
    {
        private readonly UserContext _context;
        public CartRepo(UserContext context)
        {
            _context = context;
        }

        public CartMaster GetCartMasterById(int id)
        {
            return _context.CartMaster.Where(x => x.CartMasterId == id).FirstOrDefault();
        }

        //public CartMaster GetCartMasterByUserId(int id)
        //{
        //    return _context.CartMaster.Where(x => x.UserId == id).OrderBy(x=>x.CartMasterId).LastOrDefault();
        //}

        //Cart Master
        public CartMaster AddCartMaster(CartMaster crtmast)
        {
            _context.CartMaster.Add(crtmast);
            _context.SaveChanges();
            return crtmast;
        }

        public void UpdateProduct(CartMaster oldp, CartMaster newp)
        {
            _context.Entry<CartMaster>(oldp).CurrentValues.SetValues(newp);
            _context.SaveChanges();

        }
        //If payment of previous id is 0 the it should not add further data in cartmaster table
        public CartMaster GetCartMasterByUserId(int userid)
        {
            return _context.CartMaster.Where(x => x.UserId == userid).OrderBy(x=>x.CartMasterId).LastOrDefault();
        }



        //Cart Deatil
        public CartDetail GetCartDetailById(int id)
        {
            return _context.CartDetail.FirstOrDefault(x => x.ProductId == id);
        }
        public CartDetail GetCartDetailByDetailId(int id)
        {
            return _context.CartDetail.FirstOrDefault(x => x.CartDetailId == id);
        }
        public void AddCartDetail(CartDetail cartDetail)
        {
            _context.CartDetail.Add(cartDetail);
            _context.SaveChanges();
        }

        public List<CartDetail> GetCartMasterDetils(int id)
        {
            return _context.CartDetail.Where(x=>x.CartMasterId==id).ToList();
        }
        //get cart detail so that it can't add same product again
        public CartDetail GetCartDetailByMaster_ProductId(int masterid, int productid)
        {
            return _context.CartDetail.FirstOrDefault(x => x.CartMasterId == masterid && x.ProductId == productid);
        }

        public void DeleteProduct(int masterid,int productid)
        {
           var data= _context.CartDetail.FirstOrDefault(x=>x.CartMasterId==masterid && x.ProductId == productid);
            if (data != null)
            {
                _context.CartDetail.Remove(data);
                _context.SaveChanges();
            }
           
        }

        public Cardd ValidateCard(string cardNum,string expiDate,int cvv)
        {
            var obj=_context.Card.Where(x => x.CardNumber == cardNum && x.ExpiryDate == expiDate && x.CVV == cvv).FirstOrDefault();
            return obj;
        }

        public void UpdateQuantity(CartDetail oldp, CartDetail newp)
        {
            _context.Entry<CartDetail>(oldp).CurrentValues.SetValues(newp);
            _context.SaveChanges();

        }

    }
    }

