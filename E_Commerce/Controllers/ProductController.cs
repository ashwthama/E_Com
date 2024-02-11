using E_Commerce.Services;
using E_Commerce.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _prdService;

        public ProductController(IProductServices prdService)

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

       
        ////[HttpGet("ParticularUser/{id}")]

        ////public IActionResult GetById(int id)
        ////{
        ////    try
        ////    {
        ////        var response = _prdService.GetProductByid(id);
        ////        return Ok(response);
        ////    }
        ////    catch (Exception)
        ////    {

        ////        return StatusCode(500);
        ////    }

        ////}


        
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var response = _prdService.GetProduct(id);
            return Ok(response);
        }


        
        [HttpPost]
        public IActionResult Post([FromBody] ProductVM prdVM)
        {

            var response = _prdService.AddProduct(prdVM);
            return Ok(response);

        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductVM value)
        {

            var respopnse = _prdService.UpdateDetails(id, value);
            return Ok(respopnse);
        }

        [HttpPut("ProductStock/{id}")]
        public IActionResult UpdateProductStock(int id, [FromBody] ProductStockVM value)
        {

            var respopnse = _prdService.UpdateProductStock(id, value);
            return Ok(respopnse);
        }


        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            return Ok(_prdService.DeleteProduct(id));
        }
    }
}
