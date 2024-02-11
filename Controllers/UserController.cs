using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController : ControllerBase
    {
        private UserService _db;

        public UserController(EF_DormitoryDb context, IConfiguration configuration)
        {
            _db = new UserService(context,configuration);
        }
        
        [HttpGet("ValiDateToken")]
        public async Task<ActionResult<string>> validateToken()
        {
            string token = Request.Headers["x-access-token"];
            Console.WriteLine(token);
            if (token == "" || token == null)
            {
                return Unauthorized(new { message = "Header Token missing!" });
            }
            else
            {
                bool isValid = _db.validateToken(token);
                if (isValid)
                {
                    return Ok(new { token = token });
                }
                else
                {
                    return Unauthorized(new { message = "Token invalid" });
                }
            }
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            
            IEnumerable<User> data = _db.getAllUser();
            return Ok(new{data = data});
            
        }

        [HttpGet("GetUser/{idUser}")]
        public async Task<ActionResult<string>> getUserById(string idUser)
        {
            var data = await _db.getUserById(idUser);
            return Ok(data);
        }
        [HttpGet("GetUserAllByIdroom/{idRoom}")]
        public async Task<ActionResult<string>> getUsersByIdRoom(string idRoom)
        {
            var data = await _db.getUsersByRoomId(idRoom);
            return Ok(data);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register([FromBody] UserRegister request)
        {

            if (request.password != request.confirmPassword)
            {
                return BadRequest(new { message = "รหัสผ่านไม่ตรงกัน" });
            }
            var _user = await _db.registerAsync(request);
            if (_user == null)
            {
                return BadRequest(new { message = "มีชื่อผู้ใช้นี้อยู่แล้ว" });
            }
            return CreatedAtAction("register", new { user = _user, token = _user.token });
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody]UserLogin req)
        {
            var token = await _db.loginAsync(req);
            if (token == "false")
            {
                return BadRequest(new { message = "รหัสผ่านไม่ถูกต้อง" });
            }
            else if (token == "UsernameFalse")
            {
                return BadRequest(new { message = "ชื่อผู้ใช้ไม่ถูกต้อง" });
            }
            return Ok(new { token = token });
        }

        [HttpPut("UpdateIdRoom")]
        public async Task<ActionResult<string>> updateIdRoom(CodeAddRoom res)
        {
            
            var data = await _db.updateIdRoom(res);
            return Ok(data);
        }

        [HttpDelete("DeleteUser/{idUser}")]
        public async Task<ActionResult<string>> DeleteUser(string idUser)
        {
            
            var status = await _db.DeleteUser(idUser);
            return Ok(status);
        }

    }
        
}

