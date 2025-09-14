using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
    : IdentityDbContext<AppUser>(dbContextOptions)
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Person> People { get; set; }
    
    public DbSet<AppUser> AppUsers { get; set; } //TODO: see of its necessary or not
    public DbSet<Rate> Rates { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Cast> Cast { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        List<IdentityRole> roles =
        [
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },

            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        ];
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Media>().ToTable("Media");
        modelBuilder.Entity<Movie>().ToTable("Movies");
        modelBuilder.Entity<Series>().ToTable("Series");
        modelBuilder.Entity<Season>().ToTable("Seasons");
        modelBuilder.Entity<Episode>().ToTable("Episodes");
        modelBuilder.Entity<IdentityRole>().HasData(roles);


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

        // modelBuilder.Entity<Movie>()
        //     .HasMany(m => m.Genres)
        //     .WithMany(g => g.Movies)
        //     .HasForeignKey(c => c.GenreId)
        //     .OnDelete(DeleteBehavior.Cascade);
        
        // modelBuilder.Entity<Series>()
        //     .HasMany(m => m.Genres)
        //     .WithMany(g => g.Series)
        //     .HasForeignKey(c => c.GenreId)
        //     .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity(j => j.ToTable("MovieGenres"));

        modelBuilder.Entity<Series>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Series)
            .UsingEntity(j => j.ToTable("SeriesGenres")); 
        
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Media)      
            .WithMany(m => m.Comments)  
            .HasForeignKey(c => c.MediaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Rate>()
            .HasOne(r => r.Media)     
            .WithMany(m => m.Rates)     
            .HasForeignKey(r => r.MediaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Season>()
            .HasOne(s => s.Series)
            .WithMany(sr => sr.Seasons)
            .HasForeignKey(s => s.SeriesId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Episode>()
            .HasOne(e => e.Season)
            .WithMany(s => s.Episodes)
            .HasForeignKey(e => e.SeasonId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Cast>()
            .Property(c => c.Role)
            .HasConversion<string>();
        
        modelBuilder.Entity<Rate>()
            .Property(r => r.Score)
            .HasConversion<double>();
    }
}