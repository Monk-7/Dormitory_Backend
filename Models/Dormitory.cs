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
        public string district { get; set; } = null!;
        public string province { get; set; } = null!;
        public string postalCode { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string email { get; set; } = null!;
        public DateTimeOffset? timesTamp { get; set; }

    }

    public class CreateDormitory
    {
        public string? idUser { get; set; }
        public string dormitoryName { get; set; } = null!;
        public string address { get; set; } = null!;
        public string district { get; set; } = null!;
        public string province { get; set; } = null!;
        public string postalCode { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string email { get; set; } = null!;
    }

    public class DetailDormitory
    {
        public string dormitoryName { get; set; } = null!;
        public string address { get; set; } = null!;
        public string district { get; set; } = null!;
        public string province { get; set; } = null!;
        public string postalCode { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string email { get; set; } = null!;
    }
    
    public class NameDormitory
    {
        public string dormitoryName { get; set; } = null!;
        public string idRoom { get; set; } = null!;
        public int roomName { get; set; } = 0!;


    }
}
