using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;
using OfficeOpenXml;
using System;
using System.IO;


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
        
        [HttpGet("GetMeter/{idUser}")]
        public async Task<ActionResult<string>> GetMeter(string idUser)
        {
            var data = await _db.GetMeter(idUser);
            return Ok(data);
        }

        [HttpPut("UpdateMeter")]
        public async Task<IActionResult> UpdateMeter([FromBody] List<MeterUpdate> res)
        {
            var data = await _db.UpdateMeter(res);
            return Ok(data);
        }


        [HttpPost("UploadFile")]
        public async Task<ActionResult<string>> updateImg(IFormFile file)
        {
            var result = await _db.UpdateAddFile(file);
            return Ok(result);
            
        }

        [HttpGet("ExportFile/{idUser}")]
        public async Task<ActionResult<string>> ExportFile(string idUser)
        {
            var result = await _db.ExportFile(idUser);
            return File(result.Item1, result.Item2, result.Item3);   
        }
    }
}
