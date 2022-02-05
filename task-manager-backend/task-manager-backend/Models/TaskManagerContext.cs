using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskManagerBackend.Models
{
    public partial class TaskManagerContext : DbContext
    {
        public TaskManagerContext()
        {
        }

        public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Priority> Priorities { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TaskManager;Persist Security Info=True;User ID=sa;Password=Admin123!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("Priority");

                entity.Property(e => e.PriorityID).HasColumnName("PriorityID");

                entity.Property(e => e.PriorityName).HasMaxLength(16);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusName).HasMaxLength(16);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.TaskCreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TaskDeadline).HasColumnType("date");

                entity.Property(e => e.TaskDescription).HasMaxLength(500);

                entity.Property(e => e.TaskStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.TaskTitle).HasMaxLength(25);

                entity.HasOne(d => d.AssigneeNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.Assignee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tasks__Assignee__2C3393D0");

                entity.HasOne(d => d.TaskPriorityNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskPriority)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tasks__TaskPrior__2D27B809");

                entity.HasOne(d => d.TaskStatusNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskStatus)
                    .HasConstraintName("FK__Tasks__TaskStatu__2E1BDC42");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Password).HasMaxLength(64);

                entity.Property(e => e.Username).HasMaxLength(16);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
