using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain;

public partial class WebAtrioContext : DbContext
{
    public WebAtrioContext()
    {
    }

    public WebAtrioContext(DbContextOptions<WebAtrioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonJob> PersonJobs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESK-BRE-ILI;Initial Catalog=web-atrio;Trust Server Certificate=True;Trusted_Connection=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Job__3214EC07BAA2D434");

            entity.ToTable("Job");

            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.Position).HasMaxLength(255);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PERSON__3214EC0778DB817F");

            entity.ToTable("Person");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<PersonJob>(entity =>
        {
            entity.HasKey(e => new { e.IdPerson, e.IdJob, e.StartDate }).HasName("PK__PersonJo__78EAF552FE2AA60F");

            entity.ToTable("PersonJob");

            entity.Property(e => e.IdPerson).HasColumnName("Id_Person");
            entity.Property(e => e.IdJob).HasColumnName("Id_Job");
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");
            entity.Property(e => e.EndDate).HasColumnName("End_Date");

            entity.HasOne(d => d.Job).WithMany(p => p.PersonJobs)
                .HasForeignKey(d => d.IdJob)
                .HasConstraintName("FK__PersonJob__Id_Jo__3E52440B");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonJobs)
                .HasForeignKey(d => d.IdPerson)
                .HasConstraintName("FK__PersonJob__Id_Pe__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
