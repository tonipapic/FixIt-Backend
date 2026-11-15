using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FixIt_Backend.Data.Models;

public partial class FixItContext : DbContext
{
    public FixItContext()
    {
    }

    public FixItContext(DbContextOptions<FixItContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FileUpload> FileUploads { get; set; }

    public virtual DbSet<Kategorija> Kategorijas { get; set; }

    public virtual DbSet<Prijava> Prijavas { get; set; }

    public virtual DbSet<PrijavaFileUpload> PrijavaFileUploads { get; set; }

    public virtual DbSet<Prioritet> Prioritets { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLocalAuth> UserLocalAuths { get; set; }

    public virtual DbSet<UserOauth> UserOauths { get; set; }

    public virtual DbSet<UserPrijava> UserPrijavas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=FixIt");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileUpload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FileUplo__3214EC07255FCC7A");

            entity.ToTable("FileUpload");

            entity.Property(e => e.Extension).HasMaxLength(25);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Uploaded).HasColumnType("datetime");
        });

        modelBuilder.Entity<Kategorija>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kategori__3214EC073D91ADC9");

            entity.ToTable("Kategorija");

            entity.HasIndex(e => e.Name, "UQ__Kategori__737584F6EF2BFC70").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(75);
            entity.Property(e => e.TipKategorije).HasMaxLength(100);
        });

        modelBuilder.Entity<Prijava>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prijava__3214EC07BFC689AA");

            entity.ToTable("Prijava");

            entity.Property(e => e.DatumPopravka).HasColumnType("datetime");
            entity.Property(e => e.DatumPrijave).HasColumnType("datetime");
            entity.Property(e => e.Naslov).HasMaxLength(100);

            entity.HasOne(d => d.Kategorija).WithMany(p => p.Prijavas)
                .HasForeignKey(d => d.KategorijaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prijava__Kategor__68487DD7");

            entity.HasOne(d => d.Prioritet).WithMany(p => p.Prijavas)
                .HasForeignKey(d => d.PrioritetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prijava__Priorit__693CA210");

            entity.HasOne(d => d.Status).WithMany(p => p.Prijavas)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prijava__StatusI__6754599E");
        });

        modelBuilder.Entity<PrijavaFileUpload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PrijavaF__3214EC074D10F406");

            entity.ToTable("PrijavaFileUpload");

            entity.HasOne(d => d.FileUpload).WithMany(p => p.PrijavaFileUploads)
                .HasForeignKey(d => d.FileUploadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PrijavaFi__FileU__6D0D32F4");

            entity.HasOne(d => d.Prijava).WithMany(p => p.PrijavaFileUploads)
                .HasForeignKey(d => d.PrijavaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PrijavaFi__Prija__6C190EBB");
        });

        modelBuilder.Entity<Prioritet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Priorite__3214EC0732C9D483");

            entity.ToTable("Prioritet");

            entity.Property(e => e.Name).HasMaxLength(75);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC072BB1C63D");

            entity.ToTable("Role");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3214EC07EF5743B1");

            entity.ToTable("Status");

            entity.HasIndex(e => e.Name, "UQ__Status__737584F6F3CFF286").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(75);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC078CFD0E34");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D1053412D24F33").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleId__4D94879B");
        });

        modelBuilder.Entity<UserLocalAuth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserLoca__3214EC07D6142661");

            entity.ToTable("UserLocalAuth");

            entity.HasIndex(e => e.UserId, "UQ_UserLocalAuth_UserId").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__UserLoca__536C85E40151BD22").IsUnique();

            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Salt).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.User).WithOne(p => p.UserLocalAuth)
                .HasForeignKey<UserLocalAuth>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserLocal__UserI__5165187F");
        });

        modelBuilder.Entity<UserOauth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserOAut__3214EC07AF0B4725");

            entity.ToTable("UserOAuth");

            entity.HasIndex(e => e.Provider, "UQ__UserOAut__9944610BE2044D43").IsUnique();

            entity.Property(e => e.Provider).HasMaxLength(255);
            entity.Property(e => e.ProviderUserId).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.UserOauths)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserOAuth__UserI__5535A963");
        });

        modelBuilder.Entity<UserPrijava>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPrij__3214EC07DDE81305");

            entity.ToTable("UserPrijava");

            entity.HasOne(d => d.Prijava).WithMany(p => p.UserPrijavas)
                .HasForeignKey(d => d.PrijavaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserPrija__Prija__71D1E811");

            entity.HasOne(d => d.User).WithMany(p => p.UserPrijavas)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserPrija__UserI__70DDC3D8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
