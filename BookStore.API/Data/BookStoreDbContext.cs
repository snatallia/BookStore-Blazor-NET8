using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC072D23392F");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07F7E4507F");

            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });

        modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "e70fb67b-4e70-4cc0-a321-a98f65fc18df"
                },
                 new IdentityRole
                 {
                     Name = "Administrator",
                     NormalizedName = "ADMINISTRATOR",
                     Id = "6c408cd7-1b91-4165-a61c-d62de2a7fe42"
                 }
            );
        var hasher = new PasswordHasher<ApiUser>();

        modelBuilder.Entity<ApiUser>().HasData(
            new ApiUser 
            {
                Id = "288622b7-5d7f-46e5-a0c7-ef27c344baed",
                UserName = "admin@bookstore.com",
                NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                Email = "admin@bookstore.com",
                FirstName = "Jon",
                LastName = "Smith",
                PasswordHash = hasher.HashPassword(null, "123456")
            },
            new ApiUser 
            {
                Id = "4e29398d-410f-449d-a4da-d811a3727a02",
                UserName = "user@bookstore.com",
                NormalizedUserName = "USER@BOOKSTORE.COM",
                NormalizedEmail = "USER@BOOKSTORE.COM",
                Email = "user@bookstore.com",
                FirstName = "Basia",
                LastName = "Nowak",
                PasswordHash = hasher.HashPassword(null, "123456")
            }
            );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "e70fb67b-4e70-4cc0-a321-a98f65fc18df",
                    UserId = "4e29398d-410f-449d-a4da-d811a3727a02"
                },
                 new IdentityUserRole<string>
                 {
                     RoleId = "6c408cd7-1b91-4165-a61c-d62de2a7fe42",
                     UserId = "288622b7-5d7f-46e5-a0c7-ef27c344baed"
                 }
            );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
