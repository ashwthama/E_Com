using AutoMapper;
using E_Commerce.Domain.Models;
using E_Commerce.Domain.Models.Cart;
using E_Commerce.ViewModels;
using E_Commerce.ViewModels.Cart;
using E_Commerce.ViewModels.ProductViewModel;
using E_Commerce.ViewModels.UserVM;

namespace E_Commerce.MapperConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserViewModel>();

            CreateMap<UserViewModel, User>().ForMember(
                usr => usr.UserName,
                opt => opt.
                MapFrom(src => $"{"ES"}{"_"}{src.FirstName.ToUpper()}{src.LastName.Substring(0, 1).ToUpper()}{src.DOB.ToString("ddMMyy")}"));


            CreateMap<EmailVM, User>().ForMember(
                usr => usr.Password,
                opt => opt.MapFrom(src => src.password));
           
            CreateMap<Productt, ProductVM>();
            CreateMap<ProductVM, Productt>().ForMember(
                src => src.ProductId,
                opt => opt.Ignore()
                );

            CreateMap<CartMaster, CartMasterVM>().ReverseMap();
            CreateMap<CartDetail,CartDetailVM>().ReverseMap();

            CreateMap<PaymentVM, CartMaster>().ForMember(
               usr => usr.paymentStatus,
               opt => opt.MapFrom(src => src.paymentStatus));

            CreateMap<CDetailQuantVm, CartDetail>().ForMember(
               usr => usr.Quantity,
               opt => opt.MapFrom(src => src.Quantity));

            CreateMap<ProductStockVM, Productt>().ForMember(
               usr => usr.Stock,
               opt => opt.MapFrom(src => src.Stock));
        }
    }
}
