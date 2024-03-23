using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class Building
    {
        [Key]
        public string? idBuilding { get; set; }
        public string? idDormitory { get; set; }
        public string buildingName { get; set; } = null!;
        public int waterPrice { get; set; } = 0!;
        public int electricalPrice { get; set; } = 0!; 
        public DateTimeOffset? timesTamp { get; set; }

    }

    public class BuildingAndAllRoom
    {
        public string? idBuilding { get; set; }
        public string? idDormitory { get; set; }
        public string buildingName { get; set; } = null!;
        public int waterPrice { get; set; } = 0!;
        public int electricalPrice { get; set; } = 0!; 
        public DateTimeOffset? timesTamp { get; set; }
        public List<RoomName> roomAll  { get; set; } = new List<RoomName>();
        
    }

    public class BuildingDetail
    {
        
        public string buildingName { get; set; } = null!;
        public int waterPrice { get; set; } = 0!;
        public int electricalPrice { get; set; } = 0!; 

    }

    public class CreateBuilding
    {
        public string? idDormitory { get; set; }
        public string buildingName { get; set; } = null!;
        public int waterPrice { get; set; } = 0!;
        public int electricalPrice { get; set; } = 0!; 
    }


}
