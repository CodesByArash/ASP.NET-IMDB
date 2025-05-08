using System.Net.Mime;
using System.Security.Claims;
using api.Data;
using api.Enums;
using api.Interfaces;
using api.Models;
using api.Repository;
using API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly ISeriesRepository _seriesRepository;

    public CommentController(ApplicationDBContext context)
    {
        _commentRepository = new CommentRepository(context);
        _movieRepository = new MovieRepository(context);
        _seriesRepository = new SeriesRepository(context);
    }

    [Authorize]
    [HttpPost("movie/{movieId}")]
    public async Task<IActionResult> AddMovieComment([FromRoute] int movieId, [FromBody] CreateCommentRequest commentDto)
    {      
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId == null)
            return BadRequest("Invalid Credentials");
        var movie = await _movieRepository.GetByIdAsync(movieId);
        if (movie == null)
            return NotFound("Movie Not Found");
        var comment = CommentMappers.ToCommentModel(commentDto);
        comment.UserId = userId;
        comment.ContentId = movieId;
        comment.ContentType = ContentTypeEnum.Movie;
        var createdComment = await _commentRepository.CreateAsync(comment);

        return Ok(createdComment.ToCommentDto());
    }

    [Authorize]
    [HttpPost("series/{seriesId}")]
    public async Task<IActionResult> AddSeriesComment([FromRoute] int seriesId, [FromBody] CreateCommentRequest commentDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId == null)
            return BadRequest("Invalid Credentials");
        var series = await _seriesRepository.GetByIdAsync(seriesId);
        if (series == null)
            return NotFound("Series Not Found");
        var comment = CommentMappers.ToCommentModel(commentDto);
        comment.UserId = userId;
        comment.ContentId = seriesId;
        comment.ContentType = ContentTypeEnum.Series;
        var createdComment = await _commentRepository.CreateAsync(comment);

        return Ok(createdComment.ToCommentDto());
    }

    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetMovieComments([FromRoute] int movieId)
    {
        var comments = await _commentRepository.GetContentCommentsAsync(movieId, api.Enums.ContentTypeEnum.Movie);
        var commentDtos = comments.Select(c => c.ToCommentDto());
        return Ok(commentDtos);
    }

    [HttpGet("series/{seriesId}")]
    public async Task<IActionResult> GetSeriesComments([FromRoute]  int seriesId)
    {
        var comments = await _commentRepository.GetContentCommentsAsync(seriesId, api.Enums.ContentTypeEnum.Series);
        var commentDtos = comments.Select(c => c.ToCommentDto());
        return Ok(commentDtos);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentRequest commentDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId == null)
            return BadRequest("Invalid Credentials");

        var Comment = await _commentRepository.UpdateAsync(id, commentDto, userId);
        if (Comment == null)
            return NotFound("Comment Not Found");

        return Ok(Comment.ToCommentDto());
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId == null)
            return BadRequest("Invalid Credentials");

        var comment =  await _commentRepository.DeleteAsync(id, userId);
        if (comment == null)
            return NotFound("Comment Not Found");
        return Ok(comment);
    }
}

