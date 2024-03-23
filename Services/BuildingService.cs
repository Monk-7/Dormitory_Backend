
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.EntityFrameworkCore;

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

            _building.idBuilding = Guid.NewGuid().ToString();
            _building.idDormitory = building.idDormitory;
            _building.buildingName = building.buildingName;
            _building.waterPrice = building.waterPrice;
            _building.electricalPrice = building.electricalPrice;
            _building.timesTamp = DateTimeOffset.UtcNow;

            await _context.Building.AddAsync(_building);
            await _context.SaveChangesAsync();
            
            return _building;
        }

        public async Task<Building> CreateBuilding(CreateBuilding building)
        {
            var _building = new Building();

            _building.idBuilding = Guid.NewGuid().ToString();
            _building.idDormitory = building.idDormitory;
            _building.buildingName = building.buildingName;
            _building.waterPrice = building.waterPrice;
            _building.electricalPrice = building.electricalPrice;
            _building.timesTamp = DateTimeOffset.UtcNow;

            await _context.Building.AddAsync(_building);
            await _context.SaveChangesAsync();
            
            return _building;
        }

        public async Task<List<Building>> GetAllBuildingByIdDormitory(string idDormitory)
        {
            var buildings = await _context.Building.Where(u => u.idDormitory == idDormitory).ToListAsync();
            if(buildings != null)
            {
                return buildings;
            }
            return null;
            
        }

        public async Task<BuildingDetail> GetBuildingDetail(string idBuilding)
        {
            var building = await _context.Building.FirstOrDefaultAsync(u => u.idBuilding == idBuilding);
            if(building != null)
            {
                return new BuildingDetail{
                    buildingName = building.buildingName,
                    electricalPrice = building.electricalPrice,
                    waterPrice = building.waterPrice,
                };
            }
            return null;
            
        }

        public async Task<Building> EdotBuildingDetail(BuildingDetail req,string idBuilding)
        {
            var building = await _context.Building.FirstOrDefaultAsync(u => u.idBuilding == idBuilding);
            if(building != null)
            {
                building.buildingName = req.buildingName;
                building.electricalPrice = req.electricalPrice;
                building.waterPrice = req.waterPrice;
                await _context.SaveChangesAsync();

                return building;
            }
            return null;
            
        }

        public async Task<bool> DeleteBuilding(string idBuilding)
        {
            try
            {
                var building = await _context.Building.FindAsync(idBuilding);

                if (building == null)
                {
                    
                    return false;
                }

                _context.Building.Remove(building);
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