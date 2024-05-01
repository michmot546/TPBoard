﻿using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Data
{
    public class TPBoardDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableElement> TableElements { get; set; }
        public TPBoardDbContext(DbContextOptions<TPBoardDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new {pu.ProjectId, pu.UsertId});
            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.Project)
                .WithMany(pu => pu.Users)
                .HasForeignKey(pu => pu.ProjectId);
            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.User)
                .WithMany(pu => pu.Projects)
                .HasForeignKey(pu => pu.UsertId);

            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };

        }

    }
}
