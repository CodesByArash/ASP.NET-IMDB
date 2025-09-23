using api.Models;
using api.Repository;
using Bogus;

namespace Tests;

public static class GenreDataHelpers
{
    
    private static readonly Faker<Genre> _genreFaker;

    static GenreDataHelpers()
    {
        Randomizer.Seed = new Random(12345);

        _genreFaker = new Faker<Genre>()
            .RuleFor(g => g.Id, f => f.IndexFaker + 1)
            .RuleFor(g => g.Title, f => f.PickRandom(new[]
            {
                "Action", "Comedy", "Drama", "Horror", "Sci-Fi",
                "Romance", "Thriller", "Fantasy", "Animation", "Documentary"
            }));
    }
    public static List<Genre> CreateGenres(int count) => _genreFaker.Generate(count);

    public static Genre CreateGenre() => _genreFaker.Generate();
}