using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class MovieDatabaseContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<UserRating> UserRatings { get; set; }
    public DbSet<Cast> Casts { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("  ");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<UserRating>()
            .HasOne(ur => ur.Movie)
            .WithMany(m => m.UserRatings)
            .HasForeignKey(ur => ur.MovieId);

        modelBuilder.Entity<UserRating>()
            .HasOne(ur => ur.Series)
            .WithMany(s => s.UserRatings)
            .HasForeignKey(ur => ur.SeriesId);

        modelBuilder.Entity<UserRating>()
            .HasOne(ur => ur.Season)
            .WithMany(s => s.UserRatings)
            .HasForeignKey(ur => ur.SeasonId);

        modelBuilder.Entity<UserRating>()
            .HasOne(ur => ur.Episode)
            .WithMany(e => e.UserRatings)
            .HasForeignKey(ur => ur.EpisodeId);

        modelBuilder.Entity<UserRating>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRatings)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<Cast>()
            .HasOne(c => c.Movie)
            .WithMany(m => m.Casts)
            .HasForeignKey(c => c.MovieId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Cast>()
            .HasOne(c => c.Episode)
            .WithMany(e => e.Casts)
            .HasForeignKey(c => c.EpisodeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<User>()
            .HasMany(u => u.UserRatings)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId);
    }
}
