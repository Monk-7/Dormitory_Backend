
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

        private string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            
            // สุ่มเลือกตัวอักษรและเลขมาจาก chars
            string code = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return code;
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
        public async Task<Room> CreateOneRoom(CreateOneRoom room)
        {
            var _room = new Room();
            
            var roomData = await _context.Room.FirstOrDefaultAsync(r => r.idBuilding == room.idBuilding);
            
            if(roomData != null)
            {
                _room.idRoom = Guid.NewGuid().ToString();
                _room.idBuilding = room.idBuilding;
                _room.roomName = room.roomName;
                _room.roomPrice = roomData.roomName;
                _room.furniturePrice = roomData.furniturePrice;
                _room.internetPrice = roomData.internetPrice;
                _room.parkingPrice = roomData.parkingPrice;
                _room.timesTamp = DateTimeOffset.UtcNow;
            }

            await _context.Room.AddAsync(_room);
            await _context.SaveChangesAsync();
            
            return _room;
        }

        public async Task<List<Room>> CreateRooms(CreateRoom room)
        {
            List<Room> createdRooms = new List<Room>();

            for (int floor = 1; floor <= room.numberofFloor; floor++)
            {
                for (int roomNumber = 1; roomNumber <= room.numberofRoom; roomNumber++)
                {
                    int roomName = (floor * (int)Math.Pow(10, room.roomNumberlength - 1)) + roomNumber;

                    var _room = new Room
                    {
                        idRoom = Guid.NewGuid().ToString(),
                        idBuilding = room.idBuilding,
                        roomName = roomName,
                        roomPrice = room.roomPrice,
                        furniturePrice = room.furniturePrice,
                        internetPrice = room.internetPrice,
                        parkingPrice = room.parkingPrice,
                        timesTamp = DateTimeOffset.UtcNow
                    };

                    await _context.Room.AddAsync(_room);
                    createdRooms.Add(_room);
                }
            }

            await _context.SaveChangesAsync();

            return createdRooms;
        }
        
        // Tenant
        public async Task<Room> GetRoomByIdUser(string idUser)
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
                        roomPrice = room.roomPrice,
                        furniturePrice = room.furniturePrice,
                        internetPrice = room.internetPrice,
                        parkingPrice = room.parkingPrice,
                        timesTamp = room.timesTamp
                    };
                }
                
            }

            return null; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }

        public async Task<Room> GetRoomByIdRoom(string idRoom)
        {

            var room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == idRoom);

            // ตรวจสอบว่า room ไม่เป็น null ก่อนที่จะสร้าง Room ใหม่
            if (room != null)
            {
                return new Room
                {
                    idRoom = room.idRoom,
                    idBuilding = room.idBuilding,
                    roomName = room.roomName,
                    roomPrice = room.roomPrice,
                    furniturePrice = room.furniturePrice,
                    internetPrice = room.internetPrice,
                    parkingPrice = room.parkingPrice,
                    timesTamp = room.timesTamp
                };
            }
            return null; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }

        public async Task<List<RoomName>> GetAllRoomStatusByIdBuilding(string idBuilding)
        {
            var _rooms = new List<RoomName>();
            var chackTimes = DateTimeOffset.UtcNow;
            var rooms = await _context.Room.Where(r => r.idBuilding == idBuilding)
                        .Select(room => new RoomName {idRoom = room.idRoom,roomName = room.roomName})
                        .OrderBy(room => room.roomName).ToListAsync();
            
            // ตรวจสอบว่า room ไม่เป็น null ก่อนที่จะสร้าง Room ใหม่
            if (rooms != null)
            {
                foreach (var room in rooms)
                {
                    var user = await _context.User.FirstOrDefaultAsync(u => u.IdRoom == room.idRoom);
                    var invoice = await _context.Invoice.FirstOrDefaultAsync(i => i.idRoom == room.idRoom 
                        && i.timesTamp.Value.Month == chackTimes.Month
                        && i.timesTamp.Value.Year == chackTimes.Year);
                    if(user != null)  room.isRoomStay = true;
                    if(invoice != null && invoice.status == true) room.isRoomPay = true;
                    _rooms.Add(room);
                }

                return _rooms;
            }
            return null; // หรือ throw 404 Not Found หรือ แบบอื่น ๆ ตามความเหมาะสม
        }

        public async Task<string> CreateCode(string idRoom)
        {
            
            var codeRoom = GenerateRandomCode();
            while (true)
            {
                var existingCodeRoom = await _context.CodeRoom.FirstOrDefaultAsync(c => c.codeRoom == codeRoom);
                if(existingCodeRoom != null) codeRoom = GenerateRandomCode();
                else break;
            } 

            var _codeRoom = new CodeRoom();
            _codeRoom.idCodeRoom = Guid.NewGuid().ToString();
            _codeRoom.idRoom = idRoom;
            _codeRoom.codeRoom = codeRoom;
            _codeRoom.timesTamp = DateTimeOffset.UtcNow;

            await _context.CodeRoom.AddAsync(_codeRoom);
            await _context.SaveChangesAsync();

            return codeRoom; 
        }

        
        public async Task<List<string>> GetCode(string idRoom)
        {
            var existingCodeRoom = await _context.CodeRoom.Where(c => c.idRoom == idRoom).Select(code => code.codeRoom).ToListAsync();
            if(existingCodeRoom != null)
            {
                return existingCodeRoom;
            }

            return null; 
        }

        public async Task<string> updateRoom(UpdateRoom res)
        {
           var _room = await _context.Room.FirstOrDefaultAsync(r => r.idRoom == res.idRoom);

            if (_room != null)
            {
                _room.roomPrice = res.roomPrice;
                _room.furniturePrice = res.furniturePrice;
                _room.internetPrice = res.internetPrice;
                _room.parkingPrice = res.parkingPrice;

                await _context.SaveChangesAsync();
                
                return "Updated successfully";       
            }

            return null; // If the user is not found
        }

        public async Task<bool> DeleteOneRoom(string idRoom)
        {
            try
            {
                var room = await _context.Room.FindAsync(idRoom);

                if (room == null)
                {
                    // User not found
                    return false;
                }

                _context.Room.Remove(room);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                return false;
            }
        }

        public async Task<bool> DeleteAllRoom(string idBuilding)
        {
            try
            {
                var rooms = await _context.Room.Where(r => r.idBuilding == idBuilding).ToListAsync();

                if (rooms == null || !rooms.Any())
                {
                    // No rooms found for the provided idBuilding
                    return false;
                }

                _context.Room.RemoveRange(rooms);
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