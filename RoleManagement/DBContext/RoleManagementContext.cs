using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RoleManagement.DBContext;

public partial class RoleManagementContext : DbContext
{
    public RoleManagementContext(DbContextOptions<RoleManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerList> CustomerLists { get; set; }

    public virtual DbSet<EmployeeList> EmployeeLists { get; set; }

    public virtual DbSet<PageName> PageNames { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<StudentList> StudentLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerList>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.ToTable("CustomerList");
        });

        modelBuilder.Entity<EmployeeList>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("EmployeeList");
        });

        modelBuilder.Entity<PageName>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("PK_PageName");

            entity.Property(e => e.PageName1).HasColumnName("PageName");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("RolePermission");

            entity.HasOne(d => d.Page).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RolePermission_PageId_PageNames_PageId");

            entity.HasOne(d => d.User).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RolePermission_UserId_Users_UserId");
        });

        modelBuilder.Entity<StudentList>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.ToTable("StudentList");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
