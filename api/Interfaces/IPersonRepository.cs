using api.Dtos;
using api.Models;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces;

public interface IPersonRepository{
    public Task<List<Person>> GetAllAsync();
    public Task<Person?> GetByIdAsync(int id);
    public Task<Person> CreateAsync(PersonCreateDto personDto);
    public Task<Person?> UpdateAsync(int id, PersonUpdateDto personDto);
    public Task<Person?> DeleteAsync(int id);
}