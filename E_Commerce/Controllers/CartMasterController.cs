using E_Commerce.Services.CartSevices;
using E_Commerce.ViewModels;
using E_Commerce.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartMasterController : ControllerBase
    {

        private readonly ICartService _cartService;

        public CartMasterController(ICartService cartService)
        {
            _cartService = cartService;
        }



        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var rt = _cartService.GetAllCartDetails( id);
        //        return Ok(rt);
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(500);
        //    }

        //}


        //[HttpGet("{Userid}/userid")]
        //public IActionResult GetUserId(int Userid)
        //{
        //    var response = _cartService.GetCartMasterByUserID(Userid);
        //    return Ok(response);
        //}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = _cartService.GetAllCartDetails(id);
            return Ok(response);
        }
        [HttpPost("validateCard")]
        public IActionResult Login([FromBody] CardVM lgnvm)
        {

            var userResponse = _cartService.ValidateCardetil(lgnvm);
            return StatusCode(userResponse.StatusCode, userResponse);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CartMasterVM crtVM)
        {

            var response = _cartService.AddProduct(crtVM);
            return StatusCode(response.StatusCode,response);
        }
        
        [HttpPost("CartDetail")]
        public IActionResult PostCartDetail([FromBody] CartDetailVM crtVM)
        {

            var response = _cartService.AddCartDetails(crtVM);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(int matserid,int productid)
        {
            return Ok(_cartService.DeleteProduct(matserid, productid));
        }
        //update CartmasterVM
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PaymentVM value)
        {
            var respopnse = _cartService.UpdateCartMaster(id, value);
            return Ok(respopnse);
        }
        //CartDetail Quantity Uppdate
        [HttpPut("CartDetail/{id}")]
        public IActionResult PutQuantity(int id, [FromBody] CDetailQuantVm value)
        {
            var userResponse = _cartService.UpdateCartDetailQuant(id, value);
            return StatusCode(userResponse.StatusCode, userResponse);
        }

    }
}
