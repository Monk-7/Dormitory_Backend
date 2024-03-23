
using System.IO.Compression;
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.AspNetCore.StaticFiles;
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

        public async Task<MemoryStream> GetImgFileZip(string idCommunity)
        {
            var community = await _context.Community.FirstOrDefaultAsync(c => c.idCommunity == idCommunity);

            if(community != null)
            {
                var memoryStream = new MemoryStream(); // ย้าย MemoryStream ออกจากบล็อก using

                // สร้างไฟล์ zip
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var imgFilePath in community.imgFilePath)
                    {
                        if (File.Exists(imgFilePath))
                        {
                            // อ่านข้อมูลจากไฟล์ภาพและเพิ่มเข้าไปในไฟล์ zip
                            var basePath = Directory.GetCurrentDirectory();
                            var entryName = Path.GetFileName(Path.Combine(basePath, imgFilePath));
                            var entry = archive.CreateEntry(entryName);

                            using (var entryStream = entry.Open())
                            using (var fileStream = File.OpenRead(imgFilePath))
                            {
                                await fileStream.CopyToAsync(entryStream);
                            }
                        }
                    }
                }

                // ส่งไฟล์ zip กลับไปยังไคลเอนต์
                memoryStream.Seek(0, SeekOrigin.Begin);
                return memoryStream;
            }
            return null;
            
        }
        public async Task<List<string>> CreateImgFilePath(List<IFormFile> files)
        {

            var filePaths = new List<string>();
            foreach (var file in files)
            {
                if(!file.ContentType.Contains("image")) return null;    
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var basePath = Directory.GetCurrentDirectory();
                var fileDirectory = Path.Combine("files","img", fileName);
                var filePath = Path.Combine(basePath,"files","img", fileName);

                // บันทึกไฟล์ลงในเครื่องเซิร์ฟเวอร์
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // เพิ่มชื่อไฟล์ลงใน List ของชื่อไฟล์
                filePaths.Add(fileDirectory);
            }

            // คืนค่า List ของชื่อไฟล์ที่ถูกสร้าง
            return filePaths;
                  
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
                details = row.details,
                imgFilePath = row.imgFilePath,
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
            _community.details = res.details;
            _community.timesTamp = DateTimeOffset.UtcNow;

            await _context.Community.AddAsync(_community);
            await _context.SaveChangesAsync();
            
            return _community;
        }
        public async Task<string> CreatePostCommunity(CreateCommunity res)
        {
            var _community = new Community();

            var _idDormitory = "";
            
            
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == res.idUser);

            if (user.role == "owner" && user != null)
            {
                var dormitory = await _context.Dormitory.FirstOrDefaultAsync(b => b.idOwner == user.Id);
                if(res.category == "public" && dormitory == null){
                    _idDormitory = "";
                }
                else
                {
                    _idDormitory = dormitory.idDormitory;
                }
            }
            else if (user.role == "renter" && user != null)
            {
                var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);
                
                if(res.category == "public" && room == null){
                    _idDormitory = "";
                }
                else
                {
                    var building = await _context.Building.FirstOrDefaultAsync(b => b.idBuilding == room.idBuilding);
                    _idDormitory = building.idDormitory;
                }
            }
            
            _community.idCommunity = Guid.NewGuid().ToString();
            _community.idUser = res.idUser;
            _community.idDormitory = _idDormitory;
            _community.category = res.category;
            _community.details = res.details;
            _community.timesTamp = DateTimeOffset.UtcNow;

            await _context.Community.AddAsync(_community);
            await _context.SaveChangesAsync();
            
            return _community.idCommunity;
        }

        public async Task<bool> updateImg(List<IFormFile> files, string idCommunity)
        {
            var community = await _context.Community.FirstOrDefaultAsync(c => c.idCommunity == idCommunity);

            if(community != null)
            {
                List<string> imgFilePath = await CreateImgFilePath(files);

                community.imgFilePath = imgFilePath;

                await _context.SaveChangesAsync();

                return true;
            }
  
            return false;
        }
        public async Task<Community> CreateAnnouncement(CreateCommunity res)
        {
            var _community = new Community();
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(d => d.idOwner == res.idUser);

            if(dormitory != null)
            {
                _community.idCommunity = Guid.NewGuid().ToString();
                _community.idUser = res.idUser;
                _community.idDormitory = dormitory.idDormitory;
                _community.category = res.category;
    
                _community.details = res.details;
                _community.timesTamp = DateTimeOffset.UtcNow;

                await _context.Community.AddAsync(_community);
                await _context.SaveChangesAsync();
                
                return _community;
            }
            return null;
        }
        
        public async Task<List<string>> GetPublic()
        {

            var postAll = await _context.Community.Where(u => u.category == "public").OrderByDescending(c => c.timesTamp).Select(c => c.idCommunity).ToListAsync();
            return postAll; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }
        public async Task<GetCommunity> GetPost(string idCommunity)
        {
            
            var post = await _context.Community.FirstOrDefaultAsync(c => c.idCommunity == idCommunity);
            if(post != null)
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Id == post.idUser);
                if(user != null)
                {
                    return new GetCommunity{
                        idCommunity = post.idCommunity,
                        idUser = post.idUser,
                        fullName = user.name + ' ' + user.lastname,
                        category = post.category,
                        details = post.details,
                        timesTamp = post.timesTamp,
                    };
                }
            }
            return null; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }

        public async Task<List<string>> GetPostApartment(string idUser)
        {
            var _idDormitory = " ";

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == idUser);

            if (user != null && user.role == "owner" )
            {
                var dormitory = await _context.Dormitory.FirstOrDefaultAsync(b => b.idOwner == user.Id);
                _idDormitory = dormitory.idDormitory;
            }
            else if (user != null && user.role == "renter")
            {
                var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);
                var building = await _context.Building.FirstOrDefaultAsync(b => b.idBuilding == room.idBuilding);
                _idDormitory = building.idDormitory;
            }
            var postAll = await _context.Community.Where(u => u.category == "apartment" && u.idDormitory == _idDormitory).OrderByDescending(c => c.timesTamp).Select(c => c.idCommunity).ToListAsync();
            return postAll; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }

        public async Task<List<string>> GetAnnouncement(string idUser)
        {
            var _idDormitory = " ";

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == idUser);

            if (user != null && user.role == "owner" )
            {
                var dormitory = await _context.Dormitory.FirstOrDefaultAsync(b => b.idOwner == user.Id);
                _idDormitory = dormitory.idDormitory;
            }
            else if (user != null && user.role == "renter")
            {
                var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);
                var building = await _context.Building.FirstOrDefaultAsync(b => b.idBuilding == room.idBuilding);
                _idDormitory = building.idDormitory;
            }
            var postAll = await _context.Community.Where(u => u.category == "announcement" && u.idDormitory == _idDormitory).OrderByDescending(c => c.timesTamp).Select(c => c.idCommunity).ToListAsync();
            return postAll; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }
    }   
}