using E_Commerce.Domain.Models.Discount;
using E_Commerce.ViewModels.ResponseViewModel;

namespace E_Commerce.Services.DiscountServices
{
    public interface IDIscountService
    {
        ResponseVM AddProduct(Discount prdctVM);
        ResponseVM DeleteProduct(int id);
        List<Discount> GetAllProduct();
        Discount GetProduct(int id);
        ResponseVM UpdateDetails(int id, Discount upvm);
    }
}