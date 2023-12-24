
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
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
        
        public List<Room> GetRoomsByIdUser(string idUser)
        {
            // ค้นหา Building ที่มี idDormitory ตรงกับที่ระบุ
            var dormitory = _context.Dormitory.FirstOrDefault(u => u.idOwner == idUser);
            var building = _context.Building.FirstOrDefault(b => b.idDormitory == dormitory.idDormitory.ToString());

            if (building != null)
            {
                // ค้นหา Room ที่มี idBuilding ตรงกับ id ของ Building ที่พบ
                var rooms = _context.Room.Where(r => r.idBuilding == building.idBuilding.ToString()).ToList();

                if (rooms.Any())
                {
                    return rooms.Select(room => new Room
                    {
                        idRoom = room.idRoom,
                        idBuilding = room.idBuilding,
                        roomName = room.roomName,
                        furniturePrice = room.furniturePrice,
                        internetPrice = room.internetPrice,
                        parkingPrice = room.parkingPrice,
                        timesTamp = room.timesTamp
                    }).ToList();
                }
            }

            return null; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }

        public async Task<Room> PostRoom(Room room)
        {
            var _room = new Room();
            
            _room.idRoom = Guid.NewGuid();
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
        public Room GetRoomById(Guid idUser)
        {
            var user = _context.User.FirstOrDefault(u => u.Id == idUser);

            // ตรวจสอบว่า user ไม่เป็น null ก่อนที่จะดำเนินการต่อ
            if (user != null)
            {
                // ตรวจสอบว่า user.IdRoom มีค่าและเป็นรูปแบบของ Guid ที่ถูกต้องหรือไม่
                if (Guid.TryParse(user.IdRoom, out Guid idRoomGuid))
                {
                    var room = _context.Room.FirstOrDefault(r => r.idRoom == idRoomGuid);

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
            }

            return null; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }
        
    }   
    
}