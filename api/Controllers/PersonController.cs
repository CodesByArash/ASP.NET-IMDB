using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using api.Interfaces;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using api.Mappers;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{

    private readonly IPersonRepository _personRepository;
    public PersonController(ApplicationDBContext context)
    {
        _personRepository = new PersonRepository(context);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var people = await _personRepository.GetAllAsync();
        var personDto = people.Select(person => person.ToPersonDisplayDto()).ToList();
        return Ok(personDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail([FromRoute] int id)
    {
        var genre = await _personRepository.GetByIdAsync(id);
        if(genre == null)
            return NotFound();
        return Ok(genre.ToPersonDetailDto());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PersonCreateDto personDto){
        var person = await _personRepository.CreateAsync(personDto);
        return CreatedAtAction(nameof(GetDetail), new { id = person.Id }, person.ToPersonDisplayDto());
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PersonUpdateDto personDto){
        var person = await _personRepository.UpdateAsync(id, personDto);
        if(person == null)
            return NotFound();
        return Ok(person.ToPersonDetailDto());
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id ){
        var personModel = await _personRepository.DeleteAsync(id);
        if (personModel == null)
            return NotFound();
        return Ok(personModel.ToPersonDetailDto());
    }
}