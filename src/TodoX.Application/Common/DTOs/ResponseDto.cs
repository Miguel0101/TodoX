using System.Text.Json.Serialization;
using TodoX.Application.Common.Enums;

namespace TodoX.Application.Common.DTOs;

public class ResponseDto<T> : ResponseDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Result { get; init; }

    private ResponseDto(ErrorCode errorCode, string message, T? result = default) : base(errorCode, message)
    {
        Result = result;
    }

    public static ResponseDto<T> Success(string message, T? result = default)
        => new(ErrorCode.Success, message, result);

    public static new ResponseDto<T> Error(ErrorCode errorCode, string message)
        => new(errorCode, message);
}

public class ResponseDto
{
    public ErrorCode ErrorCode { get; init; }
    public string Message { get; init; }

    protected ResponseDto(ErrorCode errorCode, string message)
    {
        ErrorCode = errorCode;
        Message = message;
    }

    public static ResponseDto Success(string message)
        => new(ErrorCode.Success, message);

    public static ResponseDto Error(ErrorCode errorCode, string message)
        => new(errorCode, message);
}
