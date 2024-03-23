using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private InvoiceService _db;

        public InvoiceController(EF_DormitoryDb context)
        {
            _db = new InvoiceService(context);
        }

        [HttpGet("Get")]
        public IActionResult GetAllInvoices()
        {
            IEnumerable<Invoice> data = _db.GetAllInvoices();
            return Ok(new { data = data });
        }

        [HttpGet("GetInvoicesHistory/{idRoom}")]
        public async Task<IActionResult> GetInvoicesHistory(string idRoom) 
        {
            var data = await _db.GetInvoicesHistory(idRoom);
            return Ok(data);
        }

        [HttpPost("Post")]
        public async Task<ActionResult<string>> PostInvoice([FromBody] Invoice req)
        {
            var _invoice = await _db.PostInvoice(req);
            return Ok(new { data = _invoice });
        }

        [HttpPost("CreateInvoice")]
        public async Task<ActionResult<string>> CreateInvoiceAndGet([FromBody]CreateInvoice res) 
        {
            var _invoice = await _db.CreateInvoiceAndGet(res);
            return Ok(_invoice);
        }

        // [HttpPost("CreateInvoice/{idUser}")]
        // public async Task<ActionResult<string>> CreateInvoice(string idUser,DateTimeOffset dueDate) 
        // {
        //     var _meter = await _db_meter.GetAndCreateMeter(idUser);
        //     var _invoice = await _db.CreateInvoice(idUser,_meter,dueDate);
        //     return Ok(_invoice);
        // }
        [HttpPut("SendInvoice/{idUser}")]
        public async Task<ActionResult<string>> SendInvoiceToUser(string idUser) 
        {
            var _invoice = await _db.SendInvoiceToUser(idUser);
            return Ok(_invoice);
        }
        [HttpPut("Payment/{idInvoice}")]
        public async Task<ActionResult<string>> updateInvoice(string idInvoice) 
        {
            var _invoice = await _db.updateInvoice(idInvoice);
            return Ok(_invoice);
        }
    }
}
