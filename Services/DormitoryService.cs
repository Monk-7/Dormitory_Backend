
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
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

            _dormitory.idDormitory = Guid.NewGuid();
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
        
    }   
    
}