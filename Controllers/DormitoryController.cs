using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class DormitoryController : ControllerBase
    {
        private DormitoryService _db;

        public DormitoryController(EF_DormitoryDb context)
        {
            _db = new DormitoryService(context);
        }
        
        [HttpGet("Get")]
        public IActionResult Get()
        {
            
            IEnumerable<Dormitory> data = _db.GetAllDormitory();
            return Ok(new{data = data});
            
        }
        
        [HttpGet("GetAllDormitory/{idUser}")]
        public async Task<ActionResult<string>> GetAllDormitoryByIdUser(string idUser)
        {
            var data = await _db.GetAllDormitoryByIdUser(idUser);
            return Ok(data);
        }

        [HttpGet("GetDetailDormitory/{idUser}")]
        public async Task<ActionResult<string>> GetDetailDormitory(string idUser)
        {
            var data = await _db.GetDetailDormitory(idUser);
            return Ok(data);
        }

        [HttpGet("GetNameDormitory/{idUser}")]
        public async Task<ActionResult<string>> GetNameDormitory(string idUser)
        {
            var data = await _db.GetNameDormitory(idUser);
            return Ok(data);
        }

        [HttpPost("Post")]
        public async Task<ActionResult<string>> Post([FromBody]Dormitory req)
        {
            
            var _dormitory = await _db.PostDormitory(req);
            return Ok(new { Dormitory = _dormitory});
            
        }

        [HttpPost("CreateDormitory")]
        public async Task<ActionResult<string>> CreateDormitory([FromBody]CreateDormitory req)
        {
            
            await _db.CreateDormitory(req);
            return Ok("CreateDormitory Successful");
            
        }

        [HttpDelete("DeleteDormitory/{idBuilding}")]
        public async Task<ActionResult<string>> DeleteDormitory(string idDormitory)
        {
            
            var status = await _db.DeleteDormitory(idDormitory);
            return Ok(status);
        }
      
    }
        
}

