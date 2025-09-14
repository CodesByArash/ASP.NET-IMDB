using api.Enums;

namespace api.Dtos;

public class RateDto
{
    public int Id { get; set; }
    public int MediaId { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public ScoreEnum Score { get; set; }
} 