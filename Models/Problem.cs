using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class Problem
    {
        [Key]
        public string? idProblem { get; set; }
        public string? idRoom { get; set; }
        public string? idUser { get; set; }
        public string category { get; set; } = null!;
        public string title { get; set; } = null!;
        public string details { get; set; } = null!;

        public DateTimeOffset? timesTamp { get; set; }
        
    }

    public class CreateProblem
    {
        public string? idUser { get; set; }
        public string category { get; set; } = null!;
        public string title { get; set; } = null!;
        public string details { get; set; } = null!;

    }

    
}
