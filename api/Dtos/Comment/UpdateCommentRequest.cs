using api.Enums;
using api.Models;

namespace API.Dtos;

public class UpdateCommentRequest{
    public required string Text { get; set; }
}