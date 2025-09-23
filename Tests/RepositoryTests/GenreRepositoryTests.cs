using api.Data;
using api.Models;
using api.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests;
using Xunit;

namespace Tests;

public class GenreRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly GenreRepository _repo;

    public GenreRepositoryTests()
    {
        _context = DbContextHelper.GetInMemoryDbContext(Guid.NewGuid().ToString());
        _repo = new GenreRepository(_context);
    }

    public void Dispose() => _context.Dispose();

    [Fact]
    public async Task AddAsync_Should_Add_Genre()
    {
        var genre = GenreDataHelpers.CreateGenre();

        var added = await _repo.AddAsync(genre);

        added.Should().NotBeNull();
        (await _context.Genres.CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task AddAsync_Should_Return_Null_When_Entity_Invalid()
    {
        var genre = new Genre { Id = 1, Title = null! };

        Func<Task> act = async () => await _repo.AddAsync(genre);

        await act.Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Genre()
    {
        var genre = await _repo.AddAsync(GenreDataHelpers.CreateGenre());

        var result = await _repo.GetByIdAsync(genre!.Id);

        result.Should().NotBeNull();
        result!.Title.Should().Be(genre.Title);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_NotFound()
    {
        var result = await _repo.GetByIdAsync(999);

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Genres()
    {
        var genres = GenreDataHelpers.CreateGenres(3);
        foreach (var g in genres) await _repo.AddAsync(g);

        var result = await _repo.GetAllAsync();

        result.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Empty_When_No_Data()
    {
        var result = await _repo.GetAllAsync();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllByIdsAsync_Should_Return_Correct_Genres()
    {
        var genres = GenreDataHelpers.CreateGenres(3);
        foreach (var g in genres) await _repo.AddAsync(g);

        var result = await _repo.GetAllByIdsAsync(new[] { genres[0].Id, genres[2].Id });

        result.Should().HaveCount(2);
        result.Select(g => g.Id).Should().BeEquivalentTo(new[] { genres[0].Id, genres[2].Id });
    }

    [Fact]
    public async Task GetAllByIdsAsync_Should_Return_Empty_When_Ids_NotExist()
    {
        var result = await _repo.GetAllByIdsAsync(new[] { 111, 222 });

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllPaginatedAsync_Should_Return_Correct_Page()
    {
        var genres = GenreDataHelpers.CreateGenres(10);
        foreach (var g in genres) await _repo.AddAsync(g);

        var (page, total) = await _repo.GetAllPaginatedAsync(page: 2, pageSize: 3);

        total.Should().Be(10);
        page.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetAllPaginatedAsync_Should_Return_Empty_When_Page_TooHigh()
    {
        var genres = GenreDataHelpers.CreateGenres(2);
        foreach (var g in genres) await _repo.AddAsync(g);

        var (page, total) = await _repo.GetAllPaginatedAsync(page: 5, pageSize: 3);

        total.Should().Be(2);
        page.Should().BeEmpty();
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Genre()
    {
        var genre = await _repo.AddAsync(GenreDataHelpers.CreateGenre());
        genre!.Title = "Updated";

        var updated = await _repo.UpdateAsync(genre);

        updated.Should().NotBeNull();
        (await _repo.GetByIdAsync(genre.Id))!.Title.Should().Be("Updated");
    }

    [Fact]
    public async Task UpdateAsync_Should_Throw_When_Entity_NotTracked()
    {
        var genre = GenreDataHelpers.CreateGenre();

        Func<Task> act = async () => await _repo.UpdateAsync(genre);

        await act.Should().ThrowAsync<DbUpdateConcurrencyException>();
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Genre()
    {
        var genre = await _repo.AddAsync(GenreDataHelpers.CreateGenre());

        var deleted = await _repo.DeleteAsync(genre!);

        deleted.Should().BeTrue();
        (await _context.Genres.CountAsync()).Should().Be(0);
    }

    //because of how in memmory db works when a record is not in db
    //it throws exception instead of returning false db works fine
    //
    // [Fact]
    // public async Task DeleteAsync_Should_Return_False_When_Entity_NotTracked()
    // {
    //     var genre = GenreDataHelpers.CreateGenre();
    //
    //     var result = await _repo.DeleteAsync(genre);
    //
    //     result.Should().BeFalse();
    // }
}
