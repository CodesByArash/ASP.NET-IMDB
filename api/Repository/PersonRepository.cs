using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class PersonRepository : IPersonRepository{

    private readonly ApplicationDBContext _context;

    public PersonRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<List<Person>> GetAllAsync(){
        return await _context.People.ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(int id){
        return await _context.People.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Person> CreateAsync(PersonCreateDto personDto){
        var person = personDto.ToPersonModel();
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<Person?> UpdateAsync(int id, PersonUpdateDto personDto){
        var existingPerson = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
        if (existingPerson == null){
            return null;
        }
        
        existingPerson.BirthDate = personDto.BirthDate;
        existingPerson.Bio = personDto.Bio;
        existingPerson.PhotoUrl = personDto.PhotoUrl;
        existingPerson.FullName = personDto.FullName;
        existingPerson.ImdbId = personDto.ImdbId;

        await _context.SaveChangesAsync();

        return existingPerson;
    }

    public async Task<Person?> DeleteAsync(int id ){
        var person = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
        if(person == null){
            return null;
        }
        _context.People.Remove(person);
        await _context.SaveChangesAsync();

        return person;
    }
}