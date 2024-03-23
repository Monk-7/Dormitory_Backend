
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DormitoryAPI.Services
{
    public class DormitoryService
    {
        private EF_DormitoryDb _context;
        public DormitoryService(EF_DormitoryDb context)
        {
            _context = context;
        }

        public List<Dormitory> GetAllDormitory()
        {
            List<Dormitory> response = new List<Dormitory>();
            var dataList = _context.Dormitory.ToList();
            dataList.ForEach(row => response.Add(new Dormitory()
            {
                idDormitory = row.idDormitory,
                idOwner = row.idOwner,
                dormitoryName = row.dormitoryName,
                address = row.address,
                phoneNumber = row.phoneNumber,
                email = row.email,
                timesTamp = row.timesTamp
            }));
            return response;
        }
        public async Task<Dormitory> PostDormitory(Dormitory dormitory)
        {
            var _dormitory = new Dormitory();

            _dormitory.idDormitory = Guid.NewGuid().ToString();
            _dormitory.idOwner = dormitory.idOwner;
            _dormitory.dormitoryName = dormitory.dormitoryName;
            _dormitory.address = dormitory.address;
            _dormitory.phoneNumber = dormitory.phoneNumber;
            _dormitory.email = dormitory.email;
            _dormitory.timesTamp = DateTimeOffset.UtcNow;

            await _context.Dormitory.AddAsync(_dormitory);
            await _context.SaveChangesAsync();
            
            return _dormitory;
        }

        public async Task<Dormitory> CreateDormitory(CreateDormitory dormitory)
        {
            var _dormitory = new Dormitory();

            _dormitory.idDormitory = Guid.NewGuid().ToString();
            _dormitory.idOwner = dormitory.idUser;
            _dormitory.dormitoryName = dormitory.dormitoryName;
            _dormitory.address = dormitory.address;
            _dormitory.district = dormitory.district;
            _dormitory.province = dormitory.province;
            _dormitory.postalCode = dormitory.postalCode;
            _dormitory.phoneNumber = dormitory.phoneNumber;
            _dormitory.email = dormitory.email;
            _dormitory.timesTamp = DateTimeOffset.UtcNow;

            await _context.Dormitory.AddAsync(_dormitory);
            await _context.SaveChangesAsync();
            
            return _dormitory;
        }
        public async Task<DetailDormitory> GetDetailDormitory(string idUser)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == idUser);
            if(user != null)
            {
                var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);
                if(room != null)
                {
                    var building = await _context.Building.FirstOrDefaultAsync(b => b.idBuilding == room.idBuilding);
                    if(building != null)
                    {
                        var dormitory = await _context.Dormitory.FirstOrDefaultAsync(d => d.idDormitory == building.idDormitory);
                        return new DetailDormitory{
                            dormitoryName = dormitory.dormitoryName,
                            address = dormitory.address,
                            district = dormitory.district,
                            province = dormitory.province,
                            postalCode = dormitory.postalCode,
                            phoneNumber = dormitory.phoneNumber,
                            email = dormitory.email,
                        };
                    }
                }
            }
            
            return null;
        }

        public async Task<DetailDormitory> GetDetailDormitoryById(string idDormitory)
        {
           
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(d => d.idDormitory == idDormitory);
            if(dormitory != null) {
                return new DetailDormitory{
                    dormitoryName = dormitory.dormitoryName,
                    address = dormitory.address,
                    district = dormitory.district,
                    province = dormitory.province,
                    postalCode = dormitory.postalCode,
                    phoneNumber = dormitory.phoneNumber,
                    email = dormitory.email,
                };
            }        
            return null;
        }

        

        public async Task<NameDormitory> GetNameDormitory(string idUser)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == idUser);
            if(user != null)
            {
                var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);
                if(room != null)
                {
                    var building = await _context.Building.FirstOrDefaultAsync(b => b.idBuilding == room.idBuilding);
                    if(building != null)
                    {
                        var dormitory = await _context.Dormitory.FirstOrDefaultAsync(d => d.idDormitory == building.idDormitory);
                        return new NameDormitory{
                            dormitoryName = dormitory.dormitoryName,
                            idRoom = room.idRoom,
                            roomName = room.roomName
                        };
                    }
                }
            }
            
            return null;
        }

        public async Task<List<Dormitory>> GetAllDormitoryByIdUser(string idUser)
        {
            var dormitorys = await _context.Dormitory.Where(u => u.idOwner == idUser).ToListAsync();

            if (dormitorys != null)
            {
                return dormitorys;
            }

            return null;
        }

        public async Task<Dormitory> EditDormitory(DetailDormitory req,string idDormitory)
        {
            
            var _dormitory = await _context.Dormitory.FirstOrDefaultAsync(d => d.idDormitory == idDormitory);
            if(_dormitory != null)
            {
                
                _dormitory.dormitoryName = req.dormitoryName;
                _dormitory.address = req.address;
                _dormitory.district = req.district;
                _dormitory.province = req.province;
                _dormitory.postalCode = req.postalCode;
                _dormitory.phoneNumber = req.phoneNumber;
                _dormitory.email = req.email;
                await _context.SaveChangesAsync();
                return _dormitory;
            }
                  
            return null;
        }

        public async Task<bool> DeleteDormitory(string idDormitory)
        {
            try
            {
                var dormitory = await _context.Dormitory.FindAsync(idDormitory);
                var buildings = await _context.Building.Where(b => b.idDormitory == dormitory.idDormitory).ToListAsync();
                if (dormitory == null)
                {
                    
                    return false;
                }
                if(buildings != null)
                {
                    foreach(var building in buildings)
                    {
                        var rooms = await _context.Room.Where(b => b.idBuilding == building.idBuilding).ToListAsync();
                        if(rooms != null)
                        {
                            _context.Room.RemoveRange(rooms);
                            
                        }
                        _context.Building.Remove(building);
                    }
                }

                _context.Dormitory.Remove(dormitory);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                return false;
            }
        }
    }       
}