using E_Commerce.Domain.Models.Sales;
using E_Commerce.Services.Slaeservice;
using E_Commerce.ViewModels.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesServices _slsService;

        public SalesController(ISalesServices slsService)

        {
            _slsService = slsService;

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = _slsService.GetSaleMasterByUserID(id);
            return Ok(response);
        }
        [HttpGet("SaleMaster /{id}")]
        public IActionResult GetSaleMasterByInvoiceId(string id)
        {
            var response = _slsService.GetSaleMasterByInvoiceId(id);
            return Ok(response);
        }
        [HttpGet("SaleDetail/{Invoiceid}")]
        public IActionResult GetSaleDetailBYInvoiceId(string Invoiceid)
        {
            var response = _slsService.GetSaleDetailByInvoiceId(Invoiceid);
            return Ok(response);
        }

        [HttpPost("SalesMaster")]
        public IActionResult SalesMaster([FromBody] SalesMaster uservm)
        {
            var userResponse =  _slsService.AddSalesMaster(uservm);
            return StatusCode(userResponse.StatusCode, userResponse);
        }

        [HttpPost("SalesDetail")]
        public IActionResult SalesDetail([FromBody] SaleDetailVM uservm)
        {
            var userResponse = _slsService.AddSalesDetail(uservm);
            return StatusCode(userResponse.StatusCode, userResponse);
        }
        //delete the saledetail by invoice id because of cancel order
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var userResponse = _slsService.DeleteSaleDetailByInviceID(id);
            return StatusCode(userResponse.StatusCode, userResponse);
        }
    }
    }

