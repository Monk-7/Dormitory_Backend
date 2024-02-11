
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

        public async Task<List<Dormitory>> GetAllDormitoryByIdUser(string idUser)
        {
            var dormitorys = await _context.Dormitory.Where(u => u.idOwner == idUser).ToListAsync();

            if (dormitorys != null)
            {
                return dormitorys;
            }

            return null;
        }

        public async Task<bool> DeleteDormitory(string idDormitory)
        {
            try
            {
                var dormitory = await _context.Dormitory.FindAsync(idDormitory);

                if (dormitory == null)
                {
                    // User not found
                    return false;
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