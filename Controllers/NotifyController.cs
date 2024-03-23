using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
     [Route("Api/[controller]")]
    public class NotifyController : ControllerBase
    {

        private NotifyService _db;

        public NotifyController(EF_DormitoryDb context)
        {
            _db = new NotifyService(context);
        }
        [HttpGet("GetNotify/{idUser}")]
        public async Task<IActionResult> GetNotify(string idUser)
        {
            var result = await _db.GetNotify(idUser);
            return Ok(result);
        }
        [HttpGet("GetLengthNotify/{idUser}")]
        public async Task<IActionResult> GetNotifyLength(string idUser)
        {
            var result = await _db.GetNotifyLength(idUser);
            return Ok(result);
        }

        [HttpPost("CreateNotifyAnnouncement")]
        public async Task<IActionResult> CreateNotifyAnnouncement(CreateNotify req)
        {
            var result = await _db.CreateNotifyAnnouncement(req);
            return Ok(result);
        }
        [HttpPost("CreateNotifyReport")]
        public async Task<IActionResult> CreateNotifyReport(CreateNotifyReport req)
        {
            var result = await _db.CreateNotifyReport(req);
            return Ok(result);
        }
        [HttpPost("CreateNotifyPayment/{idUser}")]
        public async Task<IActionResult> CreateNotifyPayment(string idUser)
        {
            var result = await _db.CreateNotifyPayment(idUser);
            return Ok(result);
        }
        [HttpPost("CreateNotifyJoinDorm/{idUser}")]
        public async Task<IActionResult> CreateNotifyJoinDorm(string idUser)
        {
            var result = await _db.CreateNotifyJoinDorm(idUser);
            return Ok(result);
        }
        [HttpPost("CreateNotifySendInvoice/{idUser}")]
        public async Task<IActionResult> CreateNotifySendInvoice(string idUser)
        {
            var result = await _db.CreateNotifySendInvoice(idUser);
            return Ok(result);
        }
        
        
    }
}