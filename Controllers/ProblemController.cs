using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProblemController : ControllerBase
    {
        private ProblemService _db;

        public ProblemController(EF_DormitoryDb context)
        {
            _db = new ProblemService(context);
        }

        [HttpGet("Get")]
        public IActionResult GetAllProblems()
        {
            IEnumerable<Problem> data = _db.GetAllProblems();
            return Ok(new { data = data });
        }

        [HttpGet("GetProblemAllByIdRoom/{idRoom}")]
        public async Task<ActionResult<string>> GetAllProblemsByIdRoom(string idRoom)
        {
            var _problem = await _db.GetAllProblemsByIdRoom(idRoom);
            return Ok(_problem);
        }

        [HttpPost("Post")]
        public async Task<ActionResult<string>> PostProblem([FromBody] Problem req)
        {
            var _problem = await _db.PostProblem(req);
            return Ok(new { data = _problem });
        }

        [HttpPost("CreateProblem")]
        public async Task<ActionResult<string>> CreateProblem([FromBody] CreateProblem req)
        {
            var _problem = await _db.CreateProblem(req);
            return Ok(_problem);
        }

        
    }
}
