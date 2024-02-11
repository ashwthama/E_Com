using E_Commerce.Domain.Models;

namespace E_Commerce.Repository.ProductRepository
{
    public interface IProductRepo
    {
        void AddProduct(Productt prdct);
        void DeleteProduct(Productt emp);
        List<Productt> GetProducts();
        Productt GetUserProductById(int prdId);
        void UpdateProduct(Productt oldp, Productt newp);
    }
}