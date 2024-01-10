using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class BuildingController : ControllerBase
    {
        private BuildingService _db;

        public BuildingController(EF_DormitoryDb context)
        {
            _db = new BuildingService(context);
        }
        
        [HttpGet("Get")]
        public IActionResult Get()
        {
            
            IEnumerable<Building> data = _db.GetAllBuilding();
            return Ok(new{data = data});
            
        }

        [HttpPost("Post")]

        public async Task<ActionResult<string>> Post([FromBody]Building req)
        {
            
            var _building = await _db.PostBuilding(req);
            return Ok(new { Building = _building});
            
        }

        [HttpPost("CreateBuilding")]
        public async Task<ActionResult<string>> PostCreateBuildingAndRoom([FromBody]CreateBuilding req)
        {
            
            var _building = await _db.CreateBuildingAndAllRooms(req);
            return Ok(new { Building = _building});
            
        }
    }
        
}

