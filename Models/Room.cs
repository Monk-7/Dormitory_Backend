using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class Room
    {
        [Key]
        public string? idRoom { get; set; }
        public string? idBuilding { get; set; }
        public string roomName { get; set; } = null!;
        public int roomPrice { get; set; } = 0!; 
        public int furniturePrice { get; set; } = 0!;
        public int internetPrice { get; set; } = 0!;
        public int parkingPrice { get; set; } = 0!;
        
        public DateTimeOffset? timesTamp { get; set; }

    }
}
