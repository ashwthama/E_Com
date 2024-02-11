using E_Commerce.Domain.Context;
using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.UserRepo
{
    public class UserRepository : IUserRepository
    {

        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.User.ToList();
        }
       
        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public User Login(string username, string pass)
        {
            var obj = _context.User.Where(x => x.UserName == username && x.Password == pass).FirstOrDefault();
            return obj;
        }

       
        public void InsertOtp(OTPTable value)
        {

            _context.OTPTable.Add(value);
            _context.SaveChanges();

        }
        public User GetDataByID(int id)
        {
            var res = _context.User.FirstOrDefault(x => x.UserId == id);
            return res;
        }



        //validate otp
        public OTPTable ValidateOtp(string otp)
        {
            var res = _context.OTPTable.FirstOrDefault(x => x.OTP == otp);
            return res;
        }

        public User EmailVerification(string email)
        {
            var res=_context.User.FirstOrDefault(x => x.Email == email);
            return res;
        }

        public void UpdatePassword(User oldp, User newp)
        {
            newp.UserId = oldp.UserId;
            _context.Entry<User>(oldp).CurrentValues.SetValues(newp);
            _context.SaveChanges();

        }


    }
}
