using Microsoft.EntityFrameworkCore;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Data
{
    public class TPBoardDbContext : DbContext
    {
        public TPBoardDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
