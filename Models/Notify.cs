using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class Notify
    {
        [Key]
        public string? idNotify { get; set; }
        public string? idUser { get; set; }
        public string category { get; set; } = null!;
        public string title { get; set; } = null!;
        public string details { get; set; } = null!;
        public bool status { get; set; } = false!;
        public DateTimeOffset? timesTamp { get; set; }
    }

    public class CreateNotify
    {
        public string idUser { get; set; }
        public string details { get; set; } = null!;
    }

    public class CreateNotifyReport
    {
        public string idUser { get; set; }
        public string title { get; set; } = null!;
        public string details { get; set; } = null!;
    }
}
