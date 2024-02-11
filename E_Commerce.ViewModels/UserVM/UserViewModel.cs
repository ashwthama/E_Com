using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.ViewModels.UserVM
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
       
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string Mobile { get; set; }
        public string ProfileImage { get; set; }
        public DateTime DOB { get; set; }
        public string Country { get; set; }
        public string State { get; set; }



    }
}
