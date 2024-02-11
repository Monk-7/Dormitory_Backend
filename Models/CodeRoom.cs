using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class CodeRoom
    {
        [Key]
        public string? idCodeRoom { get; set; }
        public string? idRoom { get; set; }
        public string codeRoom { get; set; } = null!;
        public DateTimeOffset? timesTamp { get; set; }
    }

    public class CodeAddRoom
    {
        public string idUser { get; set; } = null!;
        public string codeRoom { get; set; } = null!;
    }

}
