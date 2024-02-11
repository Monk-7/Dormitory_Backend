
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DormitoryAPI.Services
{
    public class InvoiceService
    {
        private EF_DormitoryDb _context;
        public InvoiceService(EF_DormitoryDb context)
        {
            _context = context;
        }

        public List<Invoice> GetAllInvoices()
        {
            List<Invoice> response = new List<Invoice>();
            var dataList = _context.Invoice.ToList();
            dataList.ForEach(row => response.Add(new Invoice()
            {
                idInvoice = row.idInvoice,
                idRoom = row.idRoom,
                electricityPrice = row.electricityPrice,
                waterPrice = row.waterPrice,
                furniturePrice = row.furniturePrice,
                internetPrice = row.internetPrice,
                parkingPrice = row.parkingPrice,
                status = row.status,
                dueDate = row.dueDate,
                timesTamp = row.timesTamp
            }));
            return response;
        }

        public async Task<Invoice> PostInvoice(Invoice invoice)
        {
            var _Invoice = new Invoice();

            _Invoice.idInvoice = Guid.NewGuid().ToString();
            _Invoice.idRoom = invoice.idRoom;
            _Invoice.electricityPrice = invoice.electricityPrice;
            _Invoice.waterPrice = invoice.waterPrice;
            _Invoice.furniturePrice = invoice.furniturePrice;
            _Invoice.internetPrice = invoice.internetPrice;
            _Invoice.parkingPrice = invoice.parkingPrice;
            _Invoice.status = false;
            _Invoice.dueDate = invoice.dueDate;
            _Invoice.timesTamp = invoice.timesTamp;

            await _context.Invoice.AddAsync(_Invoice);
            await _context.SaveChangesAsync();

            return _Invoice;
        }

        public async Task<bool> CreateInvoice(string idUser,List<GetMeter> getMeter,DateTimeOffset dueDate)
        {
            var dormitory = await _context.Dormitory.FirstOrDefaultAsync(u => u.idOwner == idUser);
            var buildings = await _context.Building.Where(u => u.idDormitory == dormitory.idDormitory).ToListAsync();
            foreach (var building in buildings)
            {
                var materAll = getMeter;
                foreach(var mater in materAll)
                {
                    foreach(var meterRoom in mater.meterRoomAll)
                    {

                        var _invoice = new Invoice();
                        var room  = await _context.Room.FirstOrDefaultAsync(u => u.idRoom == meterRoom.idRoom && u.idBuilding == building.idBuilding);
                        if(room != null)
                        {
                            var invoice = await _context.Invoice.FirstOrDefaultAsync(i => i.idRoom == room.idRoom);
                            if(invoice == null)
                            {
                                _invoice.idInvoice = Guid.NewGuid().ToString();
                                _invoice.idRoom = room.idRoom;
                                _invoice.roomName = room.roomName;
                                _invoice.roomPrice = room.roomPrice;
                                _invoice.electricityPrice = Math.Abs((meterRoom.electricity ?? 0) - (meterRoom.electricityPrev ?? 0)) * building.electricalPrice;
                                _invoice.waterPrice = Math.Abs((meterRoom.water ?? 0) - (meterRoom.waterPrev ?? 0)) * building.waterPrice;
                                _invoice.furniturePrice = room.furniturePrice;
                                _invoice.internetPrice = room.internetPrice;
                                _invoice.parkingPrice = room.parkingPrice;
                                _invoice.status = false;
                                _invoice.dueDate = DateTimeOffset.UtcNow;
                                _invoice.timesTamp = DateTimeOffset.UtcNow;

                                await _context.Invoice.AddAsync(_invoice);
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                
            }

            return true;
        }

        public async Task<List<GetInvoice>> CreateInvoiceAndGet(CreateInvoice res)
        {
            var _getInvoiceAll = new List<GetInvoice>();
            DateTimeOffset startOfNextMonth = new DateTimeOffset(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month, 1, 0, 0, 0, DateTimeOffset.UtcNow.Offset);
            DateTimeOffset dueDate = startOfNextMonth.AddMonths(1).AddDays(4); // เพิ่ม 1 เดือนและเพิ่ม 4 วัน (วันที่ 5 ของเดือน)

            var dormitorys = await _context.Dormitory.Where(u => u.idOwner == res.idUser).ToListAsync();
            if (dormitorys != null)
            {
                foreach (var dormitory in dormitorys)
                {
                    var buildings = await _context.Building.Where(u => u.idDormitory == dormitory.idDormitory).ToListAsync();
                    foreach (var building in buildings)
                    {
                        var _invoiceAll = new List<Invoice>();
                        var materAll = res.meterAll;
                        foreach(var mater in materAll)
                        {
                            foreach(var meterRoom in mater.meterRoomAll)
                            {

                                var _invoice = new Invoice();
                                var room  = await _context.Room.FirstOrDefaultAsync(u => u.idRoom == meterRoom.idRoom && u.idBuilding == building.idBuilding);
                                if(room != null)
                                {
                                    var invoice = await _context.Invoice.FirstOrDefaultAsync(i => i.idRoom == room.idRoom);
                                    if(invoice == null)
                                    {
                                        var electricityPrice = Math.Abs((meterRoom.electricity ?? 0) - (meterRoom.electricityPrev ?? 0)) * building.electricalPrice;
                                        var waterPrice = Math.Abs((meterRoom.water ?? 0) - (meterRoom.waterPrev ?? 0)) * building.waterPrice;

                                        _invoice.idInvoice = Guid.NewGuid().ToString();
                                        _invoice.idRoom = room.idRoom;
                                        _invoice.roomName = room.roomName;
                                        _invoice.roomPrice = room.roomPrice;
                                        _invoice.electricityPrice = electricityPrice;
                                        _invoice.waterPrice = waterPrice;
                                        _invoice.furniturePrice = room.furniturePrice;
                                        _invoice.internetPrice = room.internetPrice;
                                        _invoice.parkingPrice = room.parkingPrice;
                                        _invoice.other = room.parkingPrice;
                                        _invoice.total = room.parkingPrice + room.roomPrice + room.furniturePrice + room.internetPrice + electricityPrice + waterPrice;
                                        _invoice.status = false;
                                        _invoice.dueDate = dueDate;
                                        _invoice.timesTamp = DateTimeOffset.UtcNow;

                                        _invoiceAll.Add(_invoice);
                                        await _context.Invoice.AddAsync(_invoice);
                                    }
                                    else _invoiceAll.Add(invoice);
                                }
                            }
                            await _context.SaveChangesAsync();
                        }
                        var _getiInvoice = new GetInvoice{
                                idDormitory = dormitory.idDormitory,
                                idBuilding = building.idBuilding,
                                dormitoryName = dormitory.dormitoryName,
                                buildingName = building.buildingName,
                                invoiceAll = _invoiceAll
                            };
                        _getInvoiceAll.Add(_getiInvoice);
                    }
                }
                return _getInvoiceAll;
            }
            return null;
        }
    }
}