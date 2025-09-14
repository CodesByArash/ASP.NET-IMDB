using api.Enums;
using api.Models;

namespace api.Models;
public class Genre : IEntity
{
    public int Id { get; set;}
    public string Title {get; set;}
    
    public ICollection<Movie> Movies { get; set; }
    public ICollection<Series> Series { get; set; }
}