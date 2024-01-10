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
            
            var _building = await _db.PostDormitory(req);
            return Ok(new { Dormitory = _building});
            
        }
    }
        
}

