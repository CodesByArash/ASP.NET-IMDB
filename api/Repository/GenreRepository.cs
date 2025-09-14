using api.Data;
using api.Interfaces;
using api.Models;
using API.Dtos;
using api.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Repository;

public class GenreRepository :Repository<Genre>, IGenreRepository{
    public GenreRepository(ApplicationDbContext context):base(context)
    {
        
    }
}