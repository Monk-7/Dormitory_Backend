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

        [HttpGet("GetAllBuilding/{idDormitory}")]
        public async Task<ActionResult<string>> GetAllBuildingByIdDormitory(string idDormitory)
        {
            
            var data = await _db.GetAllBuildingByIdDormitory(idDormitory);
            return Ok(data);
            
        }

        [HttpGet("GetBuildingDetail/{idBuilding}")]
        public async Task<ActionResult<string>> GetBuildingDetail(string idBuilding)
        {
            
            var data = await _db.GetBuildingDetail(idBuilding);
            return Ok(data);
            
        }

        [HttpPost("Post")]

        public async Task<ActionResult<string>> Post([FromBody]Building req)
        {
            
            var _building = await _db.PostBuilding(req);
            return Ok(new { Building = _building});
            
        }

        [HttpPost("CreateBuilding")]
        public async Task<ActionResult<string>> CreateBuilding([FromBody]CreateBuilding req)
        {
            var _building = await _db.CreateBuilding(req);
            return Ok(_building.idBuilding);
            
        }

        [HttpPut("EdotBuildingDetail/{idBuilding}")]
        public async Task<ActionResult<string>> EdotBuildingDetail([FromBody] BuildingDetail req,string idBuilding)
        {
            var data = await _db.EdotBuildingDetail(req,idBuilding);
            return Ok(data);
        }

        [HttpDelete("DeleteBuilding/{idBuilding}")]
        public async Task<ActionResult<string>> DeleteBuilding(string idBuilding)
        {
            
            var status = await _db.DeleteBuilding(idBuilding);
            return Ok(status);
        }
    }
        
}

