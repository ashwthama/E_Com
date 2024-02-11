using E_Commerce.Domain.Models.Discount;
using E_Commerce.Services.DiscountServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDIscountService _prdService;

        public DiscountController(IDIscountService prdService)

        {
            _prdService = prdService;

        }



        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var rt = _prdService.GetAllProduct();
                return Ok(rt);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }




        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var response = _prdService.GetProduct(id);
            return Ok(response);
        }



        [HttpPost]
        public IActionResult Post([FromBody] Discount prdVM)
        {

            var response = _prdService.AddProduct(prdVM);
            return Ok(response);

        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Discount value)
        {

            var respopnse = _prdService.UpdateDetails(id, value);
            return Ok(respopnse);
        }


        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            return Ok(_prdService.DeleteProduct(id));
        }

    }
}
