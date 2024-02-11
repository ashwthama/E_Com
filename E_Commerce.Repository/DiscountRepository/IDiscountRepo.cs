using E_Commerce.Domain.Models.Discount;

namespace E_Commerce.Repository.DiscountRepository
{
    public interface IDiscountRepo
    {
        void AddProduct(Discount prdct);
        void DeleteProduct(Discount emp);
        List<Discount> GetAllDiscountDetils();
        Discount GetUserDiscountById(int prdId);
        void UpdateProduct(Discount oldp, Discount newp);
    }
}