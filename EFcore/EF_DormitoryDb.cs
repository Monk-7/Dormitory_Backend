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
        public DbSet<Room> Room {get; set;}
        public DbSet<Meter> Meter {get; set;}
        public DbSet<Invoice> Invoice {get; set;}
        public DbSet<Problem> Problem {get; set;}
        public DbSet<Comment> Comment {get; set;}
        public DbSet<CodeRoom> CodeRoom {get; set;}
        public DbSet<Contract> Contract {get; set;}
        public DbSet<Building> Building {get; set;}
        public DbSet<Dormitory> Dormitory {get; set;}
        public DbSet<Community> Community {get; set;}


    }

}