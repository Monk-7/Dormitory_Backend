using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class Comment
    {
        [Key]
        public string? idComment { get; set; }
        public string? idCommunity { get; set; }
        public string? idUser { get; set; }
        public string details { get; set; } = null!;
        public DateTimeOffset? timesTamp { get; set; }
    }
    public class CreateComment
    {
        public string? idCommunity { get; set; }
        public string? idUser { get; set; }
        public string details { get; set; } = null!;
    }

    public class GetComment
    {
        public string? idComment { get; set; }
        public string? idCommunity { get; set; }
        public string? idUser { get; set; }
        public string fullName { get; set; } = null!;
        public string details { get; set; } = null!;
        public DateTimeOffset? timesTamp { get; set; }
    }
    
}


