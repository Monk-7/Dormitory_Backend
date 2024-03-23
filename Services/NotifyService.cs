
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace DormitoryAPI.Services
{
    public class NotifyService
    {
        private EF_DormitoryDb _context;
        public NotifyService(EF_DormitoryDb context)
        {
            _context = context;
        }

        public async Task<List<Notify>> GetNotify(string idUser)
        {
            
            var notifyAll = await _context.Notify.Where(n => n.idUser == idUser ).OrderByDescending(n => n.timesTamp).ToListAsync();
            if(notifyAll != null)
            {
                return notifyAll;
            }
            return null;
        }

        public async Task<int> GetNotifyLength(string idUser)
        {
            
            var notifyAll = await _context.Notify.Where(n => n.idUser == idUser ).OrderByDescending(n => n.timesTamp).ToListAsync();
            if(notifyAll != null)
            {
                return notifyAll.Count;
            }
            return 0;
        }

        public async Task<bool> CreateNotifyAnnouncement(CreateNotify req)
        {   
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(d => d.idOwner == req.idUser);
            if(dormitory != null)
            {
                var buildings = await _context.Building.Where(b => b.idDormitory == dormitory.idDormitory).ToListAsync();
                foreach(var building in buildings)
                {
                    var rooms = await _context.Room.Where(r => r.idBuilding == building.idBuilding).ToListAsync();
                    foreach(var room in rooms)
                    {
                        var users = await _context.User.Where(u => u.IdRoom == room.idRoom).ToListAsync();
                        foreach(var user in users)
                        {
                            var _notify = new Notify{
                                idNotify = Guid.NewGuid().ToString(),
                                idUser = user.Id,
                                category = "announcement",
                                title = "Announcement",
                                details = req.details,
                                status = false,
                                timesTamp = DateTimeOffset.UtcNow
                            };    
                            await _context.Notify.AddRangeAsync(_notify); 
                            
                        }
                    }
                }
                
                // บันทึกข้อมูลห้อง
                await _context.SaveChangesAsync();    
                return true;
            }
            return false;     
        }
        public async Task<Notify> CreateNotifyReport(CreateNotifyReport req)
        {   
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == req.idUser);
            var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);
            var building = await _context.Building.FirstOrDefaultAsync(b => b.idBuilding == room.idBuilding);
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(d => d.idDormitory == building.idDormitory);
            
            var _notify = new Notify{
                idNotify = Guid.NewGuid().ToString(),
                idUser = dormitory.idOwner,
                category = "report",
                title = req.title,
                details = "Building " + building.buildingName + " : " + room.roomName,
                status = false,
                timesTamp = DateTimeOffset.UtcNow
            };  
            await _context.Notify.AddRangeAsync(_notify);
            // บันทึกข้อมูลห้อง
            await _context.SaveChangesAsync();    
            return _notify; 
        }
        public async Task<Notify> CreateNotifyPayment(string idUser)
        {   
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == idUser);
            var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);
            var building = await _context.Building.FirstOrDefaultAsync(b => b.idBuilding == room.idBuilding);
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(d => d.idDormitory == building.idDormitory);
            
            var _notify = new Notify{
                idNotify = Guid.NewGuid().ToString(),
                idUser = dormitory.idOwner,
                category = "payment",
                title = "Payment Successful",
                details = "Building " + building.buildingName + " : " + room.roomName,
                status = false,
                timesTamp = DateTimeOffset.UtcNow
            };  
            await _context.Notify.AddRangeAsync(_notify);
            // บันทึกข้อมูลห้อง
            await _context.SaveChangesAsync();    
            return _notify; 
        }
        public async Task<Notify> CreateNotifyJoinDorm(string idUser)
        {   
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == idUser);
            var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);
            var building = await _context.Building.FirstOrDefaultAsync(b => b.idBuilding == room.idBuilding);
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(d => d.idDormitory == building.idDormitory);
            
            var _notify = new Notify{
                idNotify = Guid.NewGuid().ToString(),
                idUser = dormitory.idOwner,
                category = "join_dorm",
                title = "Join Dormitory",
                details = "Building " + building.buildingName + " : " + room.roomName,
                status = false,
                timesTamp = DateTimeOffset.UtcNow
            };  
            await _context.Notify.AddRangeAsync(_notify);
            // บันทึกข้อมูลห้อง
            await _context.SaveChangesAsync();    
            return _notify; 
        }

        public async Task<bool> CreateNotifySendInvoice(string idUser)
        {   
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(d => d.idOwner == idUser);
            if(dormitory != null)
            {
                var buildings = await _context.Building.Where(b => b.idDormitory == dormitory.idDormitory).ToListAsync();
                foreach(var building in buildings)
                {
                    var rooms = await _context.Room.Where(r => r.idBuilding == building.idBuilding).ToListAsync();
                    foreach(var room in rooms)
                    {
                        var users = await _context.User.Where(u => u.IdRoom == room.idRoom).ToListAsync();
                        foreach(var user in users)
                        {
                            var _notify = new Notify{
                                idNotify = Guid.NewGuid().ToString(),
                                idUser = user.Id,
                                category = "invoice",
                                title = "Invoice Received",
                                details = "You have received an invoice.",
                                status = false,
                                timesTamp = DateTimeOffset.UtcNow
                            };    
                            await _context.Notify.AddRangeAsync(_notify); 
                            
                        }
                    }
                }
                await _context.SaveChangesAsync();    
                return true;
            }
            return false;
        }
    }   
    
}