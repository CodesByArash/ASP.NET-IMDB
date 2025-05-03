using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDBContext : IdentityDbContext<AppUser>
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Rate> Rates { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
        : base(dbContextOptions)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name= "Admin",
                NormalizedName= "ADMIN"
            },
            new IdentityRole{
                Name= "User",
                NormalizedName= "USER"
            }
        };
        modelBuilder.Entity<IdentityRole>().HasData(roles);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Cast>()
            .HasOne(c => c.Person)
            .WithMany(p => p.Cast)
            .HasForeignKey(c => c.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Rate>()
            .HasOne(r => r.User)
            .WithMany(r => r.Rates)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cast>()
            .Property(c => c.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Cast>()
            .Property(c => c.ContentType)
            .HasConversion<string>();

        modelBuilder.Entity<Rate>()
            .Property(r => r.ContentType)
            .HasConversion<string>();

        modelBuilder.Entity<Rate>()
            .Property(r => r.Score)
            .HasConversion<string>();

        modelBuilder.Entity<Comment>()
            .Property(c => c.ContentType)
            .HasConversion<string>();
    }
}

