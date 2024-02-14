using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
     [Route("Api/[controller]")]
    public class ContractController : ControllerBase
    {

        private ContractService _db;

        public ContractController(EF_DormitoryDb context)
        {
            _db = new ContractService(context);
        }
        
        [HttpPost("uploadFilePDF/{idRoom}")]
        public async Task<IActionResult> Upload(IFormFile file,string idRoom)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File is not selected or empty.");
                }

                // เช็คประเภทของไฟล์ที่อัปโหลด (อาจต้องปรับแก้ตามความต้องการ)
                if (!file.ContentType.Contains("application/pdf"))
                {
                    return BadRequest("Only image files are allowed.");
                }

                // กำหนดตำแหน่งและชื่อไฟล์ที่จะบันทึก
                var status = await _db.CreateContract(idRoom,file);

                // คืนที่อยู่ของไฟล์ที่บันทึกเรียบร้อย
                if(status) return Ok("Successful");
                else return Ok("Unsuccessful");
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost("uploadMultiple")]
        public async Task<IActionResult> UploadMultiple([FromForm] List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return BadRequest("No file is selected or the file list is empty.");
                }

                foreach (var file in files)
                {
                    // เช็คประเภทของไฟล์ที่อัปโหลด (อาจต้องปรับแก้ตามความต้องการ)
                    if (!file.ContentType.Contains("image"))
                    {
                        return BadRequest("Only image files are allowed.");
                    }

                    // กำหนดตำแหน่งและชื่อไฟล์ที่จะบันทึก
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(@"D:\CEPP\DormitoryAPI\img", fileName);

                    // บันทึกไฟล์ลงในเครื่องเซิร์ฟเวอร์
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                return Ok("Files uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("pdf/{idRoom}")]
        public async Task<IActionResult> GetPdf(string idRoom)
        {
            // Call the ContractService to get the PDF file
            var pdfStream = await _db.GetPdf(idRoom);
            if (pdfStream == null)
            {
                return NotFound("Contract not found.");
            }

            return new FileStreamResult(pdfStream, "application/pdf");
        }

        [HttpGet("getFilePDF/{idRoom}")]
        public async Task<IActionResult> GetFile(string idRoom)
        {
            var result = await _db.GetFile(idRoom);

            return File(result.Item1, result.Item2, result.Item3);
        }

    }
}