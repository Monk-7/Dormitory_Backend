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


        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Invalid file");
                }

                // ใช้ MemoryStream เพื่อเปิด ExcelPackage จากไฟล์ที่อัปโหลด
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0; // ตั้งค่าตำแหน่งให้เป็น 0 เพื่ออ่านจากต้นไฟล์

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        Console.WriteLine(worksheet.Cells[1, 1].Value);

                        return Ok("Excel file read successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error reading Excel file: {ex.Message}");
            }
        }
    }
}
