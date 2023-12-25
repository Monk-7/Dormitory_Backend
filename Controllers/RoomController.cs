using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpGet("getrooms/{id}")]

        public IActionResult GetRooms(string id)
        {
            
            IEnumerable<Room> data = _db.GetRoomsByIdUser(id);
            return Ok(new{data = data});
            
        }

        [HttpPost("Post")]
        public async Task<ActionResult<string>> Post([FromBody]Room req)
        {
            
            var _room = await _db.PostRoom(req);
            return Ok(new { room = _room});
            
        }

        // Tenant
        [HttpGet("getroom/{id}")]

        public IActionResult GetRoom(string id)
        {
            Room data = _db.GetRoomById(id);

            if (data != null)
            {
                return Ok(new { data = data });
            }

            // หากไม่พบ Room สำหรับ id ที่กำหนด
            return NotFound();
        }
    }
        
}

