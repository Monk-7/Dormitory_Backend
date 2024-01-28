using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class Dormitory
    {
        [Key]
        public string? idDormitory{ get; set; }
        public string? idOwner { get; set; }
        public string dormitoryName { get; set; } = null!;
        public string address { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string email { get; set; } = null!;

        public DateTimeOffset? timesTamp { get; set; }

    }

    public class CreateDormitory
    {
        public string? idUser { get; set; }
        public string dormitoryName { get; set; } = null!;
        public string address { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string email { get; set; } = null!;

    }
    public class DormitoryAllBuildingAndAllRoom
    {
        public string? idDormitory{ get; set; }
        public string? idOwner { get; set; }
        public string dormitoryName { get; set; } = null!;
        public string address { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string email { get; set; } = null!;
        public DateTimeOffset? timesTamp { get; set; }
        public List<BuildingAndAllRoom> buildingAll { get; set; } = new List<BuildingAndAllRoom>();
        

    }
}
