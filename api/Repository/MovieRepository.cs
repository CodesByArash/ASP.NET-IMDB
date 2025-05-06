using api.Data;
using api.Interfaces;
using api.Models;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Repository;

public class MovieRepository : IMovieRepository{

    private readonly ApplicationDBContext _context;

    public MovieRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<List<Movie>> GetAllAsync(){
        return await _context.Movies.ToListAsync();
        // Include(c => c.Comments).
    }

    public async Task<Movie?> GetByIdAsync(int id){
        return await _context.Movies.FirstOrDefaultAsync(c => c.Id == id);
        // .Include(c => c.Comments)
    }

    public async Task<Movie> CreateAsync(Movie movie){
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie?> UpdateAsync(int id, UpdateMovieRequest movieDto){
        var existingMovie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        if (existingMovie == null){
            return null;
        }
        existingMovie.Title = movieDto.Title;
        existingMovie.ImdbId = movieDto.ImdbId;
        existingMovie.ReleaseYear = movieDto.ReleaseYear;
        existingMovie.Description = movieDto.Description;
        existingMovie.Duration = movieDto.Duration;
        existingMovie.GenreId = movieDto.GenreId;
        existingMovie.PosterUrl = movieDto.PosterUrl;
        existingMovie.Rate = movieDto.Rate;
        await _context.SaveChangesAsync();

        return existingMovie;
    }

    public async Task<Movie?> DeleteAsync(int id ){
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        if(movie == null){
            return null;
        }
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return movie;
    }

}