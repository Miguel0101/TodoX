using TodoX.Application.Common.Enums;

namespace TodoX.Application.Common.DTOs;

public class ResultDto
{
    public ErrorCode ErrorCode { get; set; }
    public string? Message { get; set; }
}