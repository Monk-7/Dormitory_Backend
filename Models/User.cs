
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? IdRoom { get; set; }
        public string name { get; set; } = null!;
        public string lastname { get; set; } = null!;
        public string passwordHash { get; set; } = null!;
        public string email { get; set; } = null!;
        public string role { get; set; } = null!;
        public int phonenumber { get; set; } = 0!;
        public string? token { get; set; } = null!;
    }
    public class UserRegister
    {        
        public string? Id { get; set; }
        public string? IdRoom { get; set; }
        public string name { get; set; } = null!;
        public string lastname { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public string? confirmPassword  { get; set; } = null!;
        public string role { get; set; } = null!;
        public int phonenumber { get; set; } = 0!;
        public string? token { get; set; } = null!;
    }
    public class UserLogin
    {        
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
    }
    
}