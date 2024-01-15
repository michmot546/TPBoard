using Microsoft.EntityFrameworkCore;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Data
{
    public class TPBoardDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Table> Tables { get; set; }
        public TPBoardDbContext(DbContextOptions<TPBoardDbContext> options) : base(options)
        {

        }
        
    }
}
