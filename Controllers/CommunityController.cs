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

        [HttpGet("images/{idCommunity}")]
        public async Task<IActionResult> GetImageZip(string idCommunity)
        {
            
            var memoryStream = await _db.GetImgFileZip(idCommunity);
            return File(memoryStream, "application/zip", "images.zip");
        }
        [HttpGet("GetPost/{idCommunity}")]
        public async Task<ActionResult<string>> GetPost(string idCommunity)
        {
            
            var data = await _db.GetPost(idCommunity);
            return Ok(data);
            
        }
        [HttpGet("GetPostPublic")]
        public async Task<ActionResult<string>> GetPostPublic()
        {
            
            var data = await _db.GetPublic();
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

        [HttpPost("CreatePost")]
        public async Task<ActionResult<string>> CreatePostCommunity([FromBody]CreateCommunity req)
        {
            var idCommunity = await _db.CreatePostCommunity(req);
            return Ok(idCommunity);
            
        }

        [HttpPut("updatePost/{idCommunity}")]
        public async Task<ActionResult<string>> updateImg([FromForm] List<IFormFile> files,string idCommunity)
        {
            var result = await _db.updateImg(files,idCommunity);
            return Ok(result);
            
        }
    }
        
}

