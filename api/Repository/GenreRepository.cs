using api.Data;
using api.Interfaces;
using api.Models;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Repository;

public class GenreRepository : IGenreRepository{

    private readonly ApplicationDBContext _context;

    public GenreRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<List<Genre>> GetAllAsync(){
        return await _context.Genres.ToListAsync();
        // Include(c => c.Comments).
    }

    public async Task<Genre?> GetByIdAsync(int id){
        return await _context.Genres.FirstOrDefaultAsync(c => c.Id == id);
        // .Include(c => c.Comments)
    }

    public async Task<Genre> CreateAsync(Genre genre){
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();
        return genre;
    }

    public async Task<Genre?> UpdateAsync(int id, UpdateGenreRequest genreDto){
        var existingGenre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
        if (existingGenre == null){
            return null;
        }
        existingGenre.Title = genreDto.Title;
        await _context.SaveChangesAsync();

        return existingGenre;
    }

    public async Task<Genre?> DeleteAsync(int id ){
        var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
        if(genre == null){
            return null;
        }
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        return genre;
    }

}