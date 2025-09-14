using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;

public class Series:Media
{
    public List<Season> Seasons { get; set; }
    public ICollection<Genre> Genres { get; set; }
    public int SeasonsCount { get; set; }
}
