using Newtonsoft.Json;
namespace DormitoryAPI.Models
{
    public class UserNoPW
    {
        public static explicit operator UserNoPW(User obj)
        {
            return JsonConvert.DeserializeObject<UserNoPW>(JsonConvert.SerializeObject(obj));
        }
        public string? Id { get; set; }
        public string? IdRoom { get; set; }
        public string name { get; set; } = null!;
        public string lastname { get; set; } = null!;
        public string email { get; set; } = null!;
        public string role { get; set; } = null!;
        public string phonenumber { get; set; } = null!;
        public string? token { get; set; } = null!;
    }
}
