using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CommentController : ControllerBase
    {
        private CommentService _db;

        public CommentController(EF_DormitoryDb context)
        {
            _db = new CommentService(context);
        }

        [HttpGet("Get")]
        public IActionResult Get()
        { 
            var data = _db.Get();
            return Ok(data); 
        }

        [HttpGet("GetAllComment/{idCommunity}")]
        public async Task<ActionResult<string>> GetAllComment(string idCommunity)
        {
            
            var data = await _db.GetAllComment(idCommunity);
            return Ok(data);
            
        }

        [HttpPost("Post")]
        public async Task<ActionResult<string>> Post([FromBody]Comment req)
        {
            
            var data = await _db.Post(req);
            return Ok(data);
        }

        [HttpPost("CreateComment")]
        public async Task<ActionResult<string>> CreateComment([FromBody]CreateComment req)
        {
            
            var data = await _db.CreateComment(req);
            return Ok(data);
            
        }
    }
}
