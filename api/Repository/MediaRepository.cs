using api.Data;
using api.Enums;
using api.Interfaces;
using api.Models;
using api.Dtos;
using api.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using api.Mappers;

namespace api.Repository;

public class MediaRepository : Repository<Media>, IMediaRepository
{
    public MediaRepository(ApplicationDbContext context) : base(context)
    {
        
    }
} 

