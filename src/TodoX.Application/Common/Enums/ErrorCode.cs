namespace TodoX.Application.Common.Enums;

public enum ErrorCode
{
    Success = 0,
    UserNotFound,
    UserAlreadyExists,
    InvalidField,
    InvalidPassword,
    TodoListNotFound,
    TodoItemNotFound,
    Unauthorized,
    UndefinedError
}