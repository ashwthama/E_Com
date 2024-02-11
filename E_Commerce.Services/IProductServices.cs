using E_Commerce.ViewModels.ProductViewModel;
using E_Commerce.ViewModels.ResponseViewModel;

namespace E_Commerce.Services
{
    public interface IProductServices
    {
        ResponseVM AddProduct(ProductVM prdctVM);
        ResponseVM DeleteProduct(int id);
        List<ProductVM> GetAllProduct();
        ProductVM GetProduct(int id);
        ResponseVM UpdateDetails(int id, ProductVM upvm);
        ResponseVM UpdateProductStock(int id, ProductStockVM upvm);
    }
}