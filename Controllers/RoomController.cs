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
        private ProblemService _db_problem;


        public RoomController(EF_DormitoryDb context)
        {
            _db = new RoomService(context);
            _db_problem = new ProblemService(context);
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
        [HttpGet("GetOneRoom/{idRoom}")]
        public async Task<ActionResult<string>> GetRoomByIdRoom(string idRoom)
        {
            Room data = await _db.GetRoomByIdRoom(idRoom);

            if (data != null)
            {
                return Ok(data);
            }

            return NotFound();
        }

        [HttpGet("GetAllRoomStatus/{idBuilding}")]
        public async Task<IActionResult> GetAllRoomStatusByIdBuilding(string idBuilding)
        {
            var data = await _db.GetAllRoomStatusByIdBuilding(idBuilding);
            return Ok(data);
        }

        [HttpGet("GetCode/{idRoom}")]
        public async Task<ActionResult<string>> GetCode(string idRoom)
        {
            var data = await _db.GetCode(idRoom);
            return Ok(data);
            
        }

        [HttpPost("Post")]
        public async Task<ActionResult<string>> Post([FromBody]Room req)
        {
            
            var _room = await _db.PostRoom(req);
            return Ok(new { room = _room});
            
        }

        [HttpPost("CreateOneRoom")]
        public async Task<ActionResult<string>> CreateOneRoom([FromBody]CreateOneRoom req)
        {
            
            var _room = await _db.CreateOneRoom(req);
            return Ok("CreateRoom Successful");
        }

        [HttpPost("CreateRoom")]
        public async Task<ActionResult<string>> CreateRooms([FromBody]CreateRoom req)
        {
            
            var _room = await _db.CreateRooms(req);
            return Ok("CreateRoom Successful");
            
        }
        
        [HttpPost("CreateCode/{idRoom}")]
        public async Task<ActionResult<string>> CreateCode(string idRoom)
        {
            var data = await _db.CreateCode(idRoom);
            return Ok(data);
            
        }

        [HttpPut("UpdateRoom")]
        public async Task<ActionResult<string>> updateRoom([FromBody]UpdateRoom res)
        {
            var data = await _db.updateRoom(res);
            return Ok(data);
            
        }

        [HttpDelete("DeleteOneRoom/{idRoom}")]
        public async Task<ActionResult<string>> DeleteOneRoom(string idRoom)
        {
            
            var status = await _db.DeleteOneRoom(idRoom);
            return Ok(status);
        }

        [HttpDelete("DeleteAllRoom/{idBuilding}")]
        public async Task<ActionResult<string>> DeleteAllRoom(string idBuilding)
        {
            
            var status = await _db.DeleteAllRoom(idBuilding);
            return Ok(status);
        }

    }
        
}

