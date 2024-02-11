using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class Room
    {
        [Key]
        public string? idRoom { get; set; }
        public string? idBuilding { get; set; }
        public int roomName { get; set; } = 0!;
        public int roomPrice { get; set; } = 0!; 
        public int furniturePrice { get; set; } = 0!;
        public int internetPrice { get; set; } = 0!;
        public int parkingPrice { get; set; } = 0!;
        public DateTimeOffset? timesTamp { get; set; }

    }

    public class CreateRoom
    {
        public string? idBuilding { get; set; }
        public int roomPrice { get; set; } = 0!; 
        public int furniturePrice { get; set; } = 0!;
        public int internetPrice { get; set; } = 0!;
        public int parkingPrice { get; set; } = 0!;
        public int roomNumberlength { get; set; } = 0!;
        public int numberofFloor { get; set; } = 0!;
        public int numberofRoom { get; set; } = 0!;

    }
    public class CreateOneRoom
    {
        public string? idBuilding { get; set; }
        public int roomName { get; set; } = 0!; 
    }

    public class RoomName
    {
        public string? idRoom { get; set; }
        public int roomName { get; set; } = 0!;
        public bool isRoomStay { get; set; } = false!;
        public bool isRoomPay { get; set; } = false!;
        public bool isRoomLatePay { get; set; } = false!;
    }
    public class RoomAndUserAndProblem
    {
        
        public Room room { get; set; } = new Room();
        public List<UserNoPW> users { get; set; } = new List<UserNoPW>();
        public List<Problem> problems { get; set; } = new List<Problem>();

    }

    public class UpdateRoom
    {
        public string? idRoom { get; set; }
        public int roomPrice { get; set; } = 0!; 
        public int furniturePrice { get; set; } = 0!;
        public int internetPrice { get; set; } = 0!;
        public int parkingPrice { get; set; } = 0!;

    }
    
}
