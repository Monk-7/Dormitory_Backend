
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;

namespace DormitoryAPI.Services
{
    public class BuildingService
    {
        private EF_DormitoryDb _context;
        public BuildingService(EF_DormitoryDb context)
        {
            _context = context;
        }

        public List<Building> GetAllBuilding()
        {
            List<Building> response = new List<Building>();
            var dataList = _context.Building.ToList();
            dataList.ForEach(row => response.Add(new Building()
            {
                idBuilding = row.idBuilding,
                idDormitory = row.idDormitory,
                buildingName = row.buildingName,
                waterPrice = row.waterPrice,
                electricalPrice = row.electricalPrice,
                timesTamp = row.timesTamp
            }));
            return response;
        }
        public async Task<Building> PostBuilding(Building building)
        {
            var _building = new Building();

            _building.idBuilding = Guid.NewGuid();
            _building.idDormitory = building.idDormitory;
            _building.buildingName = building.buildingName;
            _building.waterPrice = building.waterPrice;
            _building.electricalPrice = building.electricalPrice;
            _building.timesTamp = DateTimeOffset.UtcNow;

            await _context.Building.AddAsync(_building);
            await _context.SaveChangesAsync();
            
            return _building;
        }
        public async Task<Building> CreateBuildingAndRoom(CreateBuilding building)
        {
            var _building = new Building
            {
                idBuilding = Guid.NewGuid(),
                idDormitory = building.idDormitory,
                buildingName = building.buildingName,
                waterPrice = building.waterPrice,
                electricalPrice = building.electricalPrice,
                timesTamp = DateTimeOffset.UtcNow
            };

            await _context.Building.AddAsync(_building);
            await _context.SaveChangesAsync();  // บันทึกข้อมูลอาคารก่อน

            var rooms = Enumerable.Range(1, building.buildingFloor)
                .SelectMany(floor => Enumerable.Range(1, building.buildingRoom)
                    .Select(roomNumber => new Room
                    {
                        idRoom = Guid.NewGuid(),
                        idBuilding = _building.idBuilding.ToString(),
                        roomName = $"{(floor) * 1000 + roomNumber}".PadLeft(building.buildingRoomNumberlength, '0'),
                        furniturePrice = building.furniturePrice,
                        internetPrice = building.internetPrice,
                        parkingPrice = building.parkingPrice,
                        timesTamp = DateTimeOffset.UtcNow
                    }));

            await _context.Room.AddRangeAsync(rooms);
            await _context.SaveChangesAsync();  // บันทึกข้อมูลห้อง

            return _building;
        }
    }   
    
}