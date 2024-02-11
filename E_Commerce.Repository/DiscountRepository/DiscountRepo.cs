using E_Commerce.Domain.Context;
using E_Commerce.Domain.Models.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.DiscountRepository
{
    public class DiscountRepo : IDiscountRepo
    {
        private readonly UserContext _context;
        public DiscountRepo(UserContext context)
        {
            _context = context;
        }

        //get all Discount
        public List<Discount> GetAllDiscountDetils()
        {
            return _context.Discounts.ToList();
        }

        public Discount GetUserDiscountById(int prdId)
        {
            return _context.Discounts.Where(a => a.DiscountId == prdId).FirstOrDefault();
        }

        public void AddProduct(Discount prdct)
        {
            _context.Discounts.Add(prdct);
            _context.SaveChanges();
        }

        public void UpdateProduct(Discount oldp, Discount newp)
        {
            _context.Entry<Discount>(oldp).CurrentValues.SetValues(newp);
            _context.SaveChanges();

        }
        public void DeleteProduct(Discount emp)
        {
            _context.Discounts.Remove(emp);
            _context.SaveChanges();
        }


    }
}
