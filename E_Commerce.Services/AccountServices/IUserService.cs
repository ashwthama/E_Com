using E_Commerce.ViewModels.ResponseViewModel;
using E_Commerce.ViewModels.UserVM;

namespace E_Commerce.Services.AccountServices
{
    public interface IUserService
    {
        ResponseVM GetAllUser();
        Task<ResponseVM> Register(UserViewModel registerVm);
        ResponseVM Login(LoginVM UserLoginVm);
        ResponseVM ValidateOTP(OTPVM OTP);
        Task<ResponseVM> UpdatePassword(string email, EmailVM uservm);
        string DecodePass(string encPass);
    }
}