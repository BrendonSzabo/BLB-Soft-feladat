using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Repository
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        public DatabaseContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseInMemoryDatabase("BLBDb")
                    .UseLazyLoadingProxies();
            }

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                       .HasKey(u => u.Id); // Set the Id property as the primary key for User

            modelBuilder.Entity<Models.Task>()
                .HasKey(t => t.Id); // Set the Id property as the primary key for Task


            //Configure a Many To Many relationship between User and Task using intermediary entity UserTask
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithMany(t => t.Users)
                .UsingEntity<UserTask>(
                    j => j
                        .HasOne(ut => ut.Task)
                        .WithMany()
                        .HasForeignKey(ut => ut.TaskId),
                    j => j
                        .HasOne(ut => ut.User)
                        .WithMany()
                        .HasForeignKey(ut => ut.UserId),
                    j =>
                    {
                        j.HasKey(ut => new { ut.UserId, ut.TaskId });
                        j.ToTable("UserTask");
                    });

            modelBuilder.Entity<User>().HasData(new User[]
                {
                    new User(1,"Jani"),
                    new User(2,"Dezso"),
                });

            modelBuilder.Entity<Models.Task>().HasData(new Models.Task[]
                {
                    new Models.Task(1,"New task 1"){Date = "30"},
                    new Models.Task(2,"New task 2"){Date = "20"},
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}