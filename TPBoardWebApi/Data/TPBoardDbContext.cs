using Microsoft.EntityFrameworkCore;
using TPBoardWebApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TPBoardWebApi.Data
{
    public class TPBoardDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableElement> TableElements { get; set; }
        public DbSet<ProjectUser> ProjectUser { get; set; }

        public TPBoardDbContext(DbContextOptions<TPBoardDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.ProjectId, pu.UserId });

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.Project)
                .WithMany(p => p.Users)
                .HasForeignKey(pu => pu.ProjectId);

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(pu => pu.UserId);

            modelBuilder.Entity<ProjectUser>().ToTable("ProjectUser");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();
            modelBuilder.Entity<Project>()
            .HasMany(p => p.Tables)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
