using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DormitoryAPI.Models
{
    public class Meter
    {
        [Key]
        public string? idMeter{ get; set; }
        public string? idDormitory { get; set; }
        public string buildingName { get; set; } = null!;
        public List<MeterRoom> meterRoomAll { get; set; } = new List<MeterRoom>();
        public DateTimeOffset? timesTamp { get; set; }

    }

    public class MeterRoom
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string idMeterRoom { get; set; } = null!;
        public string idRoom { get; set; } = null!;
        public string roomName { get; set; } = null!;
        public int? electricity { get; set; }
        public int? water { get; set; }
    }

    public class MeterUpdate
    {
        public string? idMeter{ get; set; }
        public string? idDormitory { get; set; }
        public List<MeterUpdateRoom> meterRoomAll { get; set; } = new List<MeterUpdateRoom>();
    }

    public class MeterUpdateRoom
    {
        public string idMeterRoom { get; set; } = null!;
        public int? electricity { get; set; }
        public int? water { get; set; }
    }

    public class GetMeter
    {
        public string? idMeter{ get; set; }
        public string? idDormitory { get; set; }
        public string buildingName { get; set; } = null!;
        public List<MeterPrevRoom> meterRoomAll { get; set; } = new List<MeterPrevRoom>();
        public DateTimeOffset? timesTamp { get; set; }
    }

    public class MeterPrevRoom
    {
        public string idMeterRoom { get; set; } = null!;
        public string idRoom { get; set; } = null!;
        public string roomName { get; set; } = null!;
        public int? electricity { get; set; }
        public int? water { get; set; }
        public int? electricityPrev { get; set; }
        public int? waterPrev { get; set; }
    }

}


