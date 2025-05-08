using api.Enums;
using api.Models;

namespace API.Dtos;

public class CreateCommentRequest{
    public required string Text { get; set; }
}