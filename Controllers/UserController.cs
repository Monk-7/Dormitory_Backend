using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private UserService _db;

        public UserController(EF_DormitoryDb context, IConfiguration configuration)
        {
            _db = new UserService(context,configuration);
        }
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] UserRegister request)
        {

            if (request.password != request.confirmPassword)
            {
                return BadRequest(new { message = "Password must match!" });
            }
            var _user = await _db.registerAsync(request);
            if (_user == null)
            {
                return BadRequest(new { message = "User Already Exist!" });
            }
            return CreatedAtAction("register", new { user = _user, token = _user.token });
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]UserLogin req)
        {
            var token = await _db.loginAsync(req);
            if (token == "false")
            {
                return BadRequest(new { message = "Wrong Password!" });
            }
            else if (token == "UsernameFalse")
            {
                return BadRequest(new { message = "Wrong Username!" });
            }
            return Ok(new { token = token });
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            
            IEnumerable<User> data = _db.getAllUser();
            return Ok(new{data = data});
            
        }

        [HttpPut("updateIdRoom/{idUser}")]
        public async Task<ActionResult<string>> updateIdRoom(string idUser, string idRoom)
        {
            
            var data = await _db.updateIdRoom(idUser,idRoom);
            return Ok(new{data = data});
        }

    }
        
}

