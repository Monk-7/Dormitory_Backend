
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace DormitoryAPI.Services
{
    public class CommunityService
    {
        private EF_DormitoryDb _context;
        public CommunityService(EF_DormitoryDb context)
        {
            _context = context;
        }

        public List<Community> GetAllCommunity()
        {
            List<Community> response = new List<Community>();
            var dataList = _context.Community.ToList();
            dataList.ForEach(row => response.Add(new Community()
            {
                idCommunity = row.idCommunity,
                idUser = row.idUser,
                category = row.category,
                title = row.title,
                details = row.details,
                timesTamp = row.timesTamp
            }));
            return response;
        }
        public async Task<Community> PostCommunity(Community res)
        {
            var _community = new Community();

            _community.idCommunity = Guid.NewGuid().ToString();
            _community.idUser = res.idUser;
            _community.idDormitory = res.idDormitory;
            _community.category = res.category;
            _community.title = res.title;
            _community.details = res.details;
            _community.timesTamp = DateTimeOffset.UtcNow;

            await _context.Community.AddAsync(_community);
            await _context.SaveChangesAsync();
            
            return _community;
        }

        public async Task<Community> CreateCommunity(CreateCommunity res)
        {
            var _community = new Community();

            var _idDormitory = " ";

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == res.idUser);

            if (user.role == "owner" && user != null)
            {
                var dormitory = await _context.Dormitory.FirstOrDefaultAsync(b => b.idOwner == user.Id);
                _idDormitory = dormitory.idDormitory;
            }
            else if (user.role == "renter" && user != null)
            {
                var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);
                var building = await _context.Building.FirstOrDefaultAsync(b => b.idBuilding == room.idBuilding);
                _idDormitory = building.idDormitory;
            }
            
            _community.idCommunity = Guid.NewGuid().ToString();
            _community.idUser = res.idUser;
            _community.idDormitory = _idDormitory;
            _community.category = res.category;
            _community.title = res.title;
            _community.details = res.details;
            _community.timesTamp = DateTimeOffset.UtcNow;

            await _context.Community.AddAsync(_community);
            await _context.SaveChangesAsync();
            
            return _community;
        }

        public async Task<List<GetCommunity>> GetPostPublic()
        {

            var postList = new List<GetCommunity>();
            var postAll = await _context.Community.Where(u => u.category == "public").ToListAsync();

            if (postAll.Any())
            {
                foreach (var post in postAll)
                {
                    var postOne = await _context.User.FirstOrDefaultAsync(u => u.Id == post.idUser);
                    var postItem = new GetCommunity
                    {
                        idCommunity = post.idCommunity,
                        idUser = post.idUser,
                        name = postOne.name + ' ' + postOne.lastname,
                        category = post.category,
                        title = post.title,
                        details = post.details,
                        timesTamp = post.timesTamp
                    };
                    postList.Add(postItem);
                }
                return postList;
            }

            return null; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }

        public async Task<List<GetCommunity>> GetPostApartment(string idUser)
        {

            var postList = new List<GetCommunity>();
            var _community = new Community();

            var _idDormitory = " ";

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == idUser);

            if (user.role == "owner" && user != null)
            {
                var dormitory = await _context.Dormitory.FirstOrDefaultAsync(b => b.idOwner == user.Id);
                _idDormitory = dormitory.idDormitory;
            }
            else if (user.role == "renter" && user != null)
            {
                var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);
                var building = await _context.Building.FirstOrDefaultAsync(b => b.idBuilding == room.idBuilding);
                _idDormitory = building.idDormitory;
            }
            var postAll = await _context.Community.Where(u => u.category == "apartment" && u.idDormitory == _idDormitory).ToListAsync();

            if (postAll.Any())
            {
                foreach (var post in postAll)
                {
                    var postOne = await _context.User.FirstOrDefaultAsync(u => u.Id == post.idUser);
                    var postItem = new GetCommunity
                    {
                        idCommunity = post.idCommunity,
                        idUser = post.idUser,
                        name = postOne.name + ' ' + postOne.lastname,
                        category = post.category,
                        title = post.title,
                        details = post.details,
                        timesTamp = post.timesTamp
                    };
                    postList.Add(postItem);
                }
                return postList;
            }

            return null; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }
    }   
    
}