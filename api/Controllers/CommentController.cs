using System.Security.Claims;
using api.Dtos;
using API.Dtos;
using api.Interfaces;
using api.Interfaces.IRepositories;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Helpers;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMediaRepository _mediaRepository;
    public CommentController(ICommentRepository commentRepository, MediaRepository mediaRepository)
    {
        _commentRepository = commentRepository;
        _mediaRepository = mediaRepository;
    }
    [HttpGet("media/{mediaId}")]
    public async Task<IActionResult> GetMediaComments([FromRoute] int mediaId)
    {
        try
        {
            var comments = await _commentRepository.GetMediaCommentsAsync(mediaId);
            var commentDtoList = comments.Select(c => c.ToCommentDto());
            return ApiResponse.Success(commentDtoList, "Comments retrieved successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to retrieve comments", 500);
        }
    }

    [Authorize]
    [HttpPost("media/{mediaId}")]
    public async Task<IActionResult> AddCommentToMedia(
        [FromRoute] int mediaId,
        [FromBody] CommentRequestDto commentDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return ApiResponse.Unauthorized();

            var media = await _mediaRepository.GetByIdAsync(mediaId);
            if (media == null)
                return ApiResponse.NotFound("Media not found");

            var comment = new Comment
            {
                Text = commentDto.Text,
                MediaId = mediaId,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                LastUpdatedOn = DateTime.UtcNow
            };
            await _commentRepository.AddAsync(comment);

            return ApiResponse.Success(comment.ToCommentDto(), "Comment added successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to add comment", 500);
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentRequestDto commentDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
                return ApiResponse.Unauthorized("Invalid Credentials");
            
            var comment = await _commentRepository.GetByIdAsync(id);
            if(comment == null)
                return ApiResponse.NotFound("Comment Not Found");
            
            var isAllowed = comment.UserId == userId;
            if (!isAllowed)
                return ApiResponse.Unauthorized("Invalid Credentials");
            
            comment.Text = commentDto.Text;
            comment.LastUpdatedOn = DateTime.UtcNow;
            var updatedComment = await _commentRepository.UpdateAsync(comment);
            if (updatedComment == null)
                return ApiResponse.NotFound("Comment Not Found");
            
            return ApiResponse.Success(comment.ToCommentDto(), "Comment updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to update comment", 500);
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
                return ApiResponse.BadRequest("Invalid Credentials");
            
            var comment = await _commentRepository.GetByIdAsync(id);
            if(comment == null)
                return ApiResponse.NotFound("Comment Not Found");
            
            var isAllowed = comment.UserId == userId;
            if (!isAllowed)
                return ApiResponse.BadRequest("Invalid Credentials");
            
            var isRemoved = await _commentRepository.DeleteAsync(comment);
            if (!isRemoved)
                return ApiResponse.NotFound("Comment Not Found");
            
            return ApiResponse.Success(isRemoved, "Comment deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.Error("Failed to delete comment", 500);
        }
    }
}