using E_Commerce.Domain.Context;
using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.ProductRepository
{
    public class ProductRepo : IProductRepo
    {
        private readonly UserContext _context;
        public ProductRepo(UserContext context)
        {
            _context = context;
        }


        public List<Productt> GetProducts()
        {
            return _context.Product.ToList();
        }

        public Productt GetUserProductById(int prdId)
        {

            return _context.Product.Where(a => a.ProductId == prdId).FirstOrDefault();
        }

        //public List<Product> GetOnlyUserProductById(int prdId)
        //{

        //    return _context.Product.Where(a => a. == prdId).ToList();
        //}
        public void AddProduct(Productt prdct)
        {
            _context.Product.Add(prdct);
            _context.SaveChanges();
        }

        public void UpdateProduct(Productt oldp, Productt newp)
        {
            _context.Entry<Productt>(oldp).CurrentValues.SetValues(newp);
            _context.SaveChanges();

        }
        public void DeleteProduct(Productt emp)
        {
            _context.Product.Remove(emp);
            _context.SaveChanges();
        }
    }
}
