using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.ViewModels.ResponseViewModel
{
    public class ResponseVM
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string ErrorMessage { get; set; }
        public string ErrorDesc { get; set; }
        public string MoreInfo { get; set; }
    }
}

