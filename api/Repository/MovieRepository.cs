using api.Data;
using api.Enums;
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
        return await _context.Movies.Include(m=>m.Genre).ToListAsync();
        // Include(c => c.Comments).
    }

    public async Task<Movie?> GetByIdAsync(int id){
        return await _context.Movies.Include(m=>m.Genre).FirstOrDefaultAsync(c => c.Id == id);
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

        var comments = await _context.Comments.Where(c => c.ContentId == id && c.ContentType == ContentTypeEnum.Movie).ToListAsync();
        var rates = await _context.Rates.Where(r => r.ContentId == id && r.ContentType == ContentTypeEnum.Movie).ToListAsync();
        var cast = await _context.Cast.Where(c => c.ContentId == id && c.ContentType == ContentTypeEnum.Movie).ToListAsync();


        _context.Cast.RemoveRange(cast);
        _context.Comments.RemoveRange(comments);
        _context.Rates.RemoveRange(rates);

        _context.Movies.Remove(movie);

        await _context.SaveChangesAsync();

        return movie;
    }
}