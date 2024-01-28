using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class RoomController : ControllerBase
    {
        private RoomService _db;

        public RoomController(EF_DormitoryDb context)
        {
            _db = new RoomService(context);
        }
        
        [HttpGet("Get")]
        public IActionResult Get()
        {
            
            IEnumerable<Room> data = _db.GetAllRoom();
            return Ok(new{data = data});
            
        }
        [HttpGet("GetAllRoom/{id}")]

        public async Task<IActionResult> GetRooms(string id)
        {
            List<Room> data = await _db.GetRoomsByIdUser(id);
            return Ok(data);
        }

        [HttpPost("Post")]
        public async Task<ActionResult<string>> Post([FromBody]Room req)
        {
            
            var _room = await _db.PostRoom(req);
            return Ok(new { room = _room});
            
        }

        // Tenant
        [HttpGet("GetOneRoom/{id}")]

        public async Task<IActionResult> GetRoom(string id)
        {
            Room data = await _db.GetRoomById(id);

            if (data != null)
            {
                return Ok(new { data = data });
            }

            return NotFound();
        }

    }
        
}

