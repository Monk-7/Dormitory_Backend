
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
            _dormitory.phoneNumber = dormitory.phoneNumber;
            _dormitory.email = dormitory.email;
            _dormitory.timesTamp = DateTimeOffset.UtcNow;

            await _context.Dormitory.AddAsync(_dormitory);
            await _context.SaveChangesAsync();
            
            return _dormitory;
        }

        public async Task<DormitoryAllBuildingAndAllRoom> GetDormitoryAndAllBuildingAndAllRooms(string idUser)
        {
            var _dormitory = new DormitoryAllBuildingAndAllRoom();
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(u => u.idOwner == idUser);
            
            if (dormitory != null)
            {
                var buildingAll = await _context.Building
                    .Where(r => r.idDormitory == dormitory.idDormitory)
                    .ToListAsync();
                
                
                foreach (var building in buildingAll)
                {
                    var roomsAll = await _context.Room
                        .Where(room => room.idBuilding == building.idBuilding)
                        .OrderBy(room => EF.Property<int>(room, "roomName"))
                        .ToListAsync();

                    var buildingAndAllRoom = new BuildingAndAllRoom
                    {
                        idBuilding = building.idBuilding,
                        idDormitory = building.idDormitory,
                        buildingName = building.buildingName,
                        waterPrice = building.waterPrice,
                        electricalPrice = building.electricalPrice,
                        roomAll = roomsAll,
                        timesTamp = building.timesTamp
                    };

                    _dormitory.buildingAll.Add(buildingAndAllRoom);
                }
                
                // เพิ่มข้อมูลที่เหลือใน _dormitory
                _dormitory.idDormitory = dormitory.idDormitory;
                _dormitory.idOwner = dormitory.idOwner;
                _dormitory.dormitoryName = dormitory.dormitoryName;
                _dormitory.address = dormitory.address;
                _dormitory.phoneNumber = dormitory.phoneNumber;
                _dormitory.email = dormitory.email;
                _dormitory.timesTamp = dormitory.timesTamp;

                return _dormitory;
            }

            return null;
        }
    }   
    
}