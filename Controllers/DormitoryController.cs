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

        [HttpPost("Post")]
        public async Task<ActionResult<string>> Post([FromBody]Dormitory req)
        {
            
            var _dormitory = await _db.PostDormitory(req);
            return Ok(new { Dormitory = _dormitory});
            
        }

        [HttpPost("CreateDormitory")]
        public async Task<ActionResult<string>> CreateDormitory([FromBody]CreateDormitory req)
        {
            
            var _dormitory = await _db.CreateDormitory(req);
            return Ok(new { Dormitory = _dormitory});
            
        }


        [HttpGet("GetDormitoryData/{idUser}")]
        public async Task<ActionResult<string>> GetDormitoryAllBuildingAndAllRooms(string idUser)
        {
            var data = await _db.GetDormitoryAndAllBuildingAndAllRooms(idUser);
            return Ok(data);
        }
    }
        
}

