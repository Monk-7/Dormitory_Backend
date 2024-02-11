
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DormitoryAPI.Services
{
    public class MeterService
    {
        private EF_DormitoryDb _context;
        public MeterService(EF_DormitoryDb context)
        {
            _context = context;
        }

        public List<Meter> GetAllMeters()
        {
            List<Meter> response = new List<Meter>();
            var dataList = _context.Meter.ToList();
            dataList.ForEach(row => response.Add(new Meter()
            {
                idMeter = row.idMeter,
                idDormitory = row.idDormitory,
                meterRoomAll = row.meterRoomAll,
                timesTamp = row.timesTamp
            }));
            return response;
        }

        public async Task<Meter> PostMeter(Meter meter)
        {
            var _meter = new Meter();

            _meter.idMeter = Guid.NewGuid().ToString();
            _meter.idDormitory = meter.idDormitory;
            _meter.meterRoomAll = meter.meterRoomAll;
            _meter.timesTamp = DateTimeOffset.UtcNow;

            await _context.Meter.AddAsync(_meter);
            await _context.SaveChangesAsync();

            return _meter;
        }

        public async Task<List<GetMeter>> GetAndCreateMeter(string idUser)
        {
            var _meter = new List<GetMeter>();
            var chackTimes = DateTimeOffset.UtcNow;
            var chackTimesMonth = chackTimes.AddMonths(-1);
            var dormitorys = await _context.Dormitory.Where(u => u.idOwner == idUser).ToListAsync();
            
            foreach (var dormitory in dormitorys)
            {

                var buildings = await _context.Building.Where(u => u.idDormitory == dormitory.idDormitory).ToListAsync();
                var existingMeter = await _context.Meter
                    .Include(m => m.meterRoomAll.OrderBy(room => room.roomName))
                    .Where(m => m.idDormitory == dormitory.idDormitory
                                && m.timesTamp.Value.Month == chackTimes.Month
                                && m.timesTamp.Value.Year == chackTimes.Year)
                    .OrderBy(m => m.buildingName)
                    .ToListAsync();

                if (existingMeter.Count != buildings.Count) 
                {
                    foreach (var building in buildings)
                    {
                        var existingMeterForBuilding = existingMeter.FirstOrDefault(m => m.idBuilding == building.idBuilding);

                        if (existingMeterForBuilding == null)
                        {
                            // ถ้าไม่มี Meter สำหรับ Building นี้ในเดือนปัจจุบัน
                            var roomsAll = await _context.Room
                                .AsNoTracking()
                                .Where(room => room.idBuilding == building.idBuilding)
                                .Select(room => new MeterRoom { idRoom = room.idRoom, roomName = room.roomName })
                                .OrderBy(room => room.roomName)
                                .ToListAsync();
                                
                            if (roomsAll == null) return null;
                            foreach (var room in roomsAll)
                            {
                                room.idMeterRoom = Guid.NewGuid().ToString();
                            }

                            // ดึง Meter จากเดือนก่อนหน้า (previous month)
                            var previousMonthMeter = await _context.Meter
                                .Include(m => m.meterRoomAll)
                                .Where(m => m.idDormitory == dormitory.idDormitory
                                            && m.timesTamp.Value.Month == chackTimesMonth.Month
                                            && m.timesTamp.Value.Year == chackTimesMonth.Year
                                            && m.buildingName == building.buildingName)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();

                            var meter = new Meter
                            {
                                idMeter = Guid.NewGuid().ToString(),
                                idDormitory = dormitory.idDormitory,
                                idBuilding = building.idBuilding,
                                dormitoryName = dormitory.dormitoryName,
                                buildingName = building.buildingName,
                                meterRoomAll = roomsAll,
                                timesTamp = DateTimeOffset.UtcNow
                            };

                            // เพิ่ม Meter ลงในฐานข้อมูล
                            await _context.Meter.AddAsync(meter);
                            await _context.SaveChangesAsync();
                            existingMeter.Add(meter);
                        }
                    }
                }
                    
                
                foreach (var building in buildings)
                {
                    var previousMonthMeter = await _context.Meter
                        .Include(m => m.meterRoomAll)
                        .Where(m => m.idDormitory == dormitory.idDormitory
                                    && m.timesTamp.Value.Month == chackTimesMonth.Month
                                    && m.timesTamp.Value.Year == chackTimesMonth.Year
                                    && m.buildingName == building.buildingName)
                        .FirstOrDefaultAsync();
    
                    if (previousMonthMeter != null)
                    {
                        // ถ้ามี Meter ในเดือนก่อนหน้า

                        foreach (var meter in existingMeter)
                        {
                            if(meter.buildingName == building.buildingName)
                            {
                                var roomsPrev = new List<MeterPrevRoom>();
                                foreach (var room in meter.meterRoomAll)
                                {
                                    var previousMonthRoom = previousMonthMeter.meterRoomAll.FirstOrDefault(r => r.idRoom == room.idRoom);

                                    var roomPrev = new MeterPrevRoom
                                    {
                                        idMeterRoom = room.idMeterRoom,
                                        idRoom = room.idRoom,
                                        roomName = room.roomName,
                                        electricity = room?.electricity,
                                        water = room?.water,
                                        electricityPrev = previousMonthRoom?.electricity,
                                        waterPrev = previousMonthRoom?.water
                                    };

                                    roomsPrev.Add(roomPrev);
                                }
                                var meterPrev = new GetMeter
                                {
                                    idMeter = meter.idMeter,
                                    idDormitory = meter.idDormitory,
                                    idBuilding = meter.idBuilding,
                                    buildingName = meter.buildingName,
                                    dormitoryName = meter.dormitoryName,
                                    meterRoomAll = roomsPrev,
                                    timesTamp = meter.timesTamp
                                };
                                _meter.Add(meterPrev);
                            }
                        }                                            
                    }
                    else
                    {
                        
                        var index = buildings.IndexOf(building);
                        var roomsPrev = new List<MeterPrevRoom>();
                        foreach (var room in existingMeter[index].meterRoomAll)
                        {

                            var roomPrev = new MeterPrevRoom
                            {
                                idMeterRoom = room.idMeterRoom,
                                idRoom = room.idRoom,
                                roomName = room.roomName,
                                electricity = room?.electricity,
                                water = room?.water,
                                electricityPrev = null,
                                waterPrev = null
                            };

                            roomsPrev.Add(roomPrev);
                        }
                        var meterPrev = new GetMeter
                        {
                            idMeter = existingMeter[index].idMeter,
                            idDormitory = existingMeter[index].idDormitory,
                            idBuilding = existingMeter[index].idBuilding,
                            buildingName = existingMeter[index].buildingName,
                            dormitoryName = existingMeter[index].dormitoryName,
                            meterRoomAll = roomsPrev,
                            timesTamp = existingMeter[index].timesTamp
                        };

                        _meter.Add(meterPrev);
                        
                    }
                }
            }
            return _meter;
        }

        public async Task<List<Meter>> UpdateMeter(List<MeterUpdate> res)
        {
            foreach (var meterUpdate in res)
            {
                var existingMeter = await _context.Meter
                    .Include(m => m.meterRoomAll)
                    .FirstOrDefaultAsync(m => m.idMeter == meterUpdate.idMeter);

                if (existingMeter != null)
                {
                    foreach (var meterUpdateRoom in meterUpdate.meterRoomAll)
                    {
                        var existingMeterRoom = existingMeter.meterRoomAll
                            .FirstOrDefault(room => room.idMeterRoom == meterUpdateRoom.idMeterRoom);

                        if (existingMeterRoom != null)
                        {
                            // อัปเดตเฉพาะ electricity และ water
                            existingMeterRoom.electricity = meterUpdateRoom.electricity;
                            existingMeterRoom.water = meterUpdateRoom.water;
                        }
                    }
                }
            }

            // บันทึกการเปลี่ยนแปลงที่เกิดขึ้นใน MeterRoom เท่านั้น
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            var _meter = new List<Meter>();
            var meterAll = await _context.Meter.Include(m => m.meterRoomAll.OrderBy(room => room.roomName)).Where(u => u.idDormitory == res[0].idDormitory).ToListAsync();
            foreach (var meter in meterAll)
            {
                _meter.Add(meter);
            }

            return _meter;
        }

        public async Task<List<Meter>> GetPrevMeter(string idUser)
        {
            var _meter = new List<Meter>();
            var chackTimes = DateTimeOffset.UtcNow;
            var getTime = chackTimes.AddMonths(-1);
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(u => u.idOwner == idUser);
            var buildings = await _context.Building.Where(u => u.idDormitory == dormitory.idDormitory).ToListAsync();
            var MeterAll = await _context.Meter.Include(m => m.meterRoomAll
                .OrderBy(room => room.roomName))
                .Where(u => u.idDormitory == dormitory.idDormitory && u.timesTamp.Value.Month == getTime.Month && u.timesTamp.Value.Year == getTime.Year).ToListAsync();
            if (MeterAll == null) return null;    
            foreach (var meter in MeterAll)
            {
                _meter.Add(meter);
            }

            return _meter;
        }
    }   
    
}