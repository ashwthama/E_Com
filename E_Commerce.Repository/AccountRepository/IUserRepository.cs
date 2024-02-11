using E_Commerce.Domain.Models;

namespace E_Commerce.Repository.UserRepo
{
    public interface IUserRepository
    {
        User GetDataByID(int id);
        void AddUser(User user);
        List<User> GetUsers();
        User Login(string username, string pass);
        void InsertOtp(OTPTable value);
        OTPTable ValidateOtp(string otp);
        User EmailVerification(string email);
        void UpdatePassword(User oldp, User newp);
    }
}