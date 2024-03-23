
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAPI.Models
{
    public class User
    {
        [Key]
        public string? Id { get; set; }
        public string? IdRoom { get; set; }
        public string name { get; set; } = null!;
        public string lastname { get; set; } = null!;
        public string passwordHash { get; set; } = null!;
        public string email { get; set; } = null!;
        public string role { get; set; } = null!;
        public string phonenumber { get; set; } = null!;
        public string? profile { get; set; } = null!;
        public string? token { get; set; } = null!;

        public DateTimeOffset? timesTamp { get; set; }
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
        public string phonenumber { get; set; } = null!;
        public string? token { get; set; } = null!;
    }
    public class UserLogin
    {        
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
    }

    public class EditProfile
    {        
        public string idUser { get; set; } = null!;
        public string name { get; set; } = null!;
        public string lastname { get; set; } = null!;
        public string email { get; set; } = null!;
        public string phonenumber { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}