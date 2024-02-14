using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace DormitoryAPI.Models
{
    public class Contract
    {
        [Key]
        public string? idContract { get; set; }
        public string idRoom { get; set; } = null!;
        public string pdfFileName { get; set; } = null!;
        public DateTimeOffset? timesTamp { get; set; }
    }

}
