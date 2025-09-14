using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Dtos;
using api.Interfaces.IRepositories;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using api.Helpers;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

    public PersonController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var people = await _personRepository.GetAllAsync();
            var personDtoList = people.Select(p => p.ToPersonDisplayDto()).ToList();
            return ApiResponse.Success(personDtoList, "People retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve people", 500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] int id)
    {
        try
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                return ApiResponse.NotFound("Person not found");

            return ApiResponse.Success(person.ToPersonDetailDto(), "Person retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve person", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PersonRequestDto personDto)
    {
        try
        {
            var person = await _personRepository.AddAsync(personDto.ToPersonModel());
            if (person == null)
                return ApiResponse.Error("Failed to create person", 500);

            return ApiResponse.Success(person.ToPersonDisplayDto(), "Person created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to create person", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PersonRequestDto personDto)
    {
        try
        {
            var person = personDto.ToPersonModel();
            person.Id = id;
            var updatedPerson = await _personRepository.UpdateAsync(person);
            if (updatedPerson == null)
                return ApiResponse.NotFound("Person not found");

            return ApiResponse.Success(updatedPerson.ToPersonDetailDto(), "Person updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to update person", 500);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var person = new Person() { Id = id };
            var isDeleted = await _personRepository.DeleteAsync(person);
            if (!isDeleted)
                return ApiResponse.NotFound("Person not found or could not be deleted");

            return ApiResponse.Success(isDeleted, "Person has been deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to delete person", 500);
        }
    }
}