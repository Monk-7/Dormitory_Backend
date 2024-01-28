
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DormitoryAPI.Services
{
    public class RoomService
    {
        private EF_DormitoryDb _context;
        public RoomService(EF_DormitoryDb context)
        {
            _context = context;
        }

        public List<Room> GetAllRoom()
        {
            List<Room> response = new List<Room>();
            var dataList = _context.Room.ToList();
            dataList.ForEach(row => response.Add(new Room()
            {
                idRoom = row.idRoom,
                idBuilding = row.idBuilding,
                roomName = row.roomName,
                furniturePrice = row.furniturePrice,
                internetPrice = row.internetPrice,
                parkingPrice = row.parkingPrice,
                timesTamp = row.timesTamp
            }));
            return response;
        }
        
        public async Task<List<Room>> GetRoomsByIdUser(string idUser)
        {
            // ค้นหา Building ที่มี idDormitory ตรงกับที่ระบุ
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(u => u.idOwner == idUser);

            if (dormitory != null)
            {
                // ค้นหา Building ที่มี idDormitory ตรงกับที่ระบุ
                var building = await _context.Building.FirstOrDefaultAsync(b => b.idDormitory == dormitory.idDormitory);

                if (building != null)
                {
                    // ค้นหา Room ที่มี idBuilding ตรงกับ id ของ Building ที่พบ
                    var rooms = await _context.Room.Where(r => r.idBuilding == building.idBuilding.ToString()).ToListAsync();

                    if (rooms.Any())
                    {
                        //เลือกข้อมูลที่ต้องการส่งคืนจาก Room
                        // var roomInfos = rooms.Select(room => new Room
                        // {
                        //     idRoom = room.idRoom,
                        //     roomName = room.roomName,
                        //     furniturePrice = room.furniturePrice,
                        //     internetPrice = room.internetPrice,
                        //     parkingPrice = room.parkingPrice,
                        //     timesTamp = room.timesTamp,
                        //     roomFloor = room.roomFloor,
                        //     roomPrice = room.roomPrice
                        // }).ToList();

                        //return roomInfos;

                        return rooms;
                    }
                }
            }

            return null; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }

        public async Task<Room> PostRoom(Room room)
        {
            var _room = new Room();
            
            _room.idRoom = Guid.NewGuid().ToString();
            _room.idBuilding = room.idBuilding;
            _room.roomName = room.roomName;
            _room.furniturePrice = room.furniturePrice;
            _room.internetPrice = room.internetPrice;
            _room.parkingPrice = room.parkingPrice;
            _room.timesTamp = DateTimeOffset.UtcNow;

            await _context.Room.AddAsync(_room);
            await _context.SaveChangesAsync();
            
            return _room;
        }
        
        // Tenant
        public async Task<Room> GetRoomById(string idUser)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == idUser);

            // ตรวจสอบว่า user ไม่เป็น null ก่อนที่จะดำเนินการต่อ
            if (user != null)
            {
                var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == user.IdRoom);

                // ตรวจสอบว่า room ไม่เป็น null ก่อนที่จะสร้าง Room ใหม่
                if (room != null)
                {
                    return new Room
                    {
                        idRoom = room.idRoom,
                        idBuilding = room.idBuilding,
                        roomName = room.roomName,
                        furniturePrice = room.furniturePrice,
                        internetPrice = room.internetPrice,
                        parkingPrice = room.parkingPrice,
                        timesTamp = room.timesTamp
                    };
                }
                
            }

            return null; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }

    }   
    
}