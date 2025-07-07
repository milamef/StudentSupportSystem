using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudentSupportDAL;

public partial class StudentSupportContext : DbContext
{
    public StudentSupportContext()
    {
    }

    public StudentSupportContext(DbContextOptions<StudentSupportContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Problem> Problems { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=StudentSupportDb;Trusted_Connection=True;");
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Major");

            entity.Property(e => e.MajorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Timer)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Problem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Problem");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Timer)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Student");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Timer)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Title)
                .HasMaxLength(4)
                .IsUnicode(false);

            entity.HasOne(d => d.Major).WithMany(p => p.Students)
                .HasForeignKey(d => d.MajorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentInMajor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
