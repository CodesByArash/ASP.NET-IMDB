using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Interfaces.IRepositories;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class PersonRepository:Repository<Person>, IPersonRepository{
    public PersonRepository(ApplicationDbContext context):base(context)
    {
        
    }
}