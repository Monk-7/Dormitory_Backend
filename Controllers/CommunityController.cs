using DormitoryAPI.Models;
using DormitoryAPI.EFcore;
using Microsoft.AspNetCore.Mvc;
using DormitoryAPI.Services;

namespace DormitoryAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CommunityController : ControllerBase
    {
        private CommunityService _db;

        public CommunityController(EF_DormitoryDb context)
        {
            _db = new CommunityService(context);
        }
        
        [HttpGet("Get")]
        public IActionResult Get()
        {
            
            IEnumerable<Community> data = _db.GetAllCommunity();
            return Ok(new{data = data});
            
        }

        [HttpGet("GetPostPublic")]
        public async Task<ActionResult<string>> GetPostPublic()
        {
            
            var data = await _db.GetPostPublic();
            return Ok(data);
            
        }
        [HttpGet("GetPostApartment/{idUser}")]
        public async Task<ActionResult<string>> GetPostApartment(string idUser)
        {
            
            var data = await _db.GetPostApartment(idUser);
            return Ok(data);
            
        }

        [HttpPost("Post")]
        public async Task<ActionResult<string>> Post([FromBody]Community req)
        {
            
            var _community = await _db.PostCommunity(req);
            return Ok(new { Community = _community});
            
        }

        [HttpPost("CreateCommunity")]
        public async Task<ActionResult<string>> CreateCommunity([FromBody]CreateCommunity req)
        {
            
            var _community = await _db.CreateCommunity(req);
            return Ok(_community);
            
        }
    }
        
}

