using Microsoft.EntityFrameworkCore;
using DormitoryAPI.Models;

namespace DormitoryAPI.EFcore
{
    public class EF_DormitoryDb : DbContext
    {
        public EF_DormitoryDb(DbContextOptions<EF_DormitoryDb> options) : base(options)
        {

        }
        public DbSet<User> User {get; set;}
    }

}