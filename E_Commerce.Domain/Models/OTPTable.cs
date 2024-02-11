using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Models
{
    public class OTPTable
    {
        public int OtpID { get; set; }
        public string OTP { get; set; }
        public DateTime OtpGenerationDatetime { get; set; }
        public DateTime OtpExpireDatetime { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
