using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class MeterController : ControllerBase
    {
        private MeterService _db;

        public MeterController(EF_DormitoryDb context)
        {
            _db = new MeterService(context);
        }

        [HttpGet("Get")]
        public IActionResult GetAllMeters()
        {
            IEnumerable<Meter> data = _db.GetAllMeters();
            return Ok(new { data = data });
        }

        [HttpPost("Post")]
        public async Task<ActionResult<string>> PostMeter([FromBody] Meter req)
        {
            var _meter = await _db.PostMeter(req);
            return Ok(new { data = _meter });
        }

        [HttpGet("GetAndCreateMeter/{idUser}")]
        public async Task<ActionResult<string>> GetAndCreateMeter(string idUser)
        {
            var data = await _db.GetAndCreateMeter(idUser);
            return Ok(data);
        }

        [HttpGet("GetPrevMeter/{idUser}")]
        public async Task<ActionResult<string>> GetPrevMeter(string idUser)
        {
            var data = await _db.GetPrevMeter(idUser);
            return Ok(data);
        }

        [HttpPut("UpdateMeter")]
        public async Task<IActionResult> UpdateMeter([FromBody] List<MeterUpdate> res)
        {
            var data = await _db.UpdateMeter(res);
            return Ok(data);
        }

    }
}
