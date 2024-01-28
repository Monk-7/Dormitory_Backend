using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class Community
    {
        [Key]
        public string? idCommunity { get; set; }
        public string? idUser { get; set; }
        public string? idDormitory { get; set; }
        public string category { get; set; } = null!;
        public string title { get; set; } = null!;
        public string details { get; set; } = null!;
        public DateTimeOffset? timesTamp { get; set; }

    }

    public class CreateCommunity
    {
        public string? idUser { get; set; }
        public string category { get; set; } = null!;
        public string title { get; set; } = null!;
        public string details { get; set; } = null!;
    }

    public class GetCommunity
    {
        public string? idCommunity { get; set; }
        public string? idUser { get; set; }
        public string name { get; set; } = null!;
        public string category { get; set; } = null!;
        public string title { get; set; } = null!;
        public string details { get; set; } = null!;
        public DateTimeOffset? timesTamp { get; set; }
    }
}
