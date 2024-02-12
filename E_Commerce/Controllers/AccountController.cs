using E_Commerce.Services.AccountServices;
using E_Commerce.ViewModels.UserVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)

        {
            _userService = userService;

        }


        // GET: api/<AccountController>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var token = Request.Headers.Authorization.ToString().Split(' ')[1];
            var rt = _userService.GetAllUser();
            return Ok(rt.Response);


        }

        // POST api/<AccountController>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel uservm)
        {


             var userResponse = await _userService.Register(uservm);


            return Ok(userResponse);

            //var userResponse = _userService.Register(uservm);
            //return Ok(userResponse.Response);


        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginVM lgnvm)
        
        {
           // { "userName": "ES_STRINGS110224",  "password": "eo6VBsQK" }

            var userResponse = _userService.Login(lgnvm);
            return StatusCode(userResponse.StatusCode, userResponse);

        }

        //[HttpPost("OtpVerify")]
        //public IActionResult VerifyOtp([FromBody] OTPVM otp)
        //{
        //    var res = _userService.ValidateOTP(otp);
        //    return StatusCode(res.StatusCode, res.Response);
        //}

        [HttpPut("{email}")]
        public async Task<IActionResult> Put(string email, [FromBody] EmailVM value)
        {

            var respopnse = await _userService.UpdatePassword(email, value);
            return Ok(respopnse);
        }

        [HttpPost("DecodePassword")]
        public IActionResult DecodePassword(string encPass)
        {
            var decodepass = _userService.DecodePass(encPass);
            return Ok(decodepass);

        }


    }
}
