using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class MovieDatabaseContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<UserRating> UserRatings { get; set; }
    public DbSet<Cast> Casts { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=Computer\\SQLEXPRESS;Initial Catalog=IMDBClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<UserRating>()
            .HasOne(ur => ur.Movie)
            .WithMany(m => m.UserRatings)
            .HasForeignKey(ur => ur.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserRating>()
            .HasOne(ur => ur.Series)
            .WithMany(s => s.UserRatings)
            .HasForeignKey(ur => ur.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cast>()
            .HasOne(c => c.Movie)
            .WithMany(m => m.Casts)
            .HasForeignKey(c => c.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cast>()
            .HasOne(c => c.Series)
            .WithMany(m => m.Casts)
            .HasForeignKey(c => c.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.UserRatings)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Casts)
            .WithOne(c => c.Person)
            .HasForeignKey(c => c.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

