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
        public List<Room> roomAll  { get; set; } = new List<Room>();
        
    }

    public class CreateBuilding
    {
        
        public string? idUser { get; set; }
        public string buildingName { get; set; } = null!;
        public int buildingRoomNumberlength { get; set; } = 0!;
        public int buildingFloor { get; set; } = 0!;
        public int buildingRoom { get; set; } = 0!;
        public int waterPrice { get; set; } = 0!;
        public int electricalPrice { get; set; } = 0!; 
        public int roomPrice { get; set; } = 0!; 
        public int furniturePrice { get; set; } = 0!; 
        public int internetPrice { get; set; } = 0!;
        public int parkingPrice { get; set; } = 0!;

    }


}
