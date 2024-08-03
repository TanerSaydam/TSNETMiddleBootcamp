using System.Text.Json.Serialization;

namespace DomainDrivenDesign.Domain.Abstractions;
public sealed class Result<T>
{
    private Result(T data)
    {
        Data = data;
        StatusCode = 200;
        IsSuccess = true;
    }

    private Result(string errorMessage, bool isSuccessful, int statusCode)
    {
        ErrorMessages = new() { errorMessage };
        IsSuccess = isSuccessful;
        StatusCode = statusCode;
    }

    private Result(List<string> errorMessages, bool isSuccessful, int statusCode)
    {
        ErrorMessages = errorMessages;
        IsSuccess = isSuccessful;
        StatusCode = statusCode;
    }
    public T? Data { get; init; }
    public bool IsSuccess { get; init; }
    public List<string>? ErrorMessages { get; init; }

    [JsonIgnore]
    public int StatusCode { get; private set; }

    public static Result<T> Success(T data)
    {
        return new(data);
    }

    public static Result<T> Failure(string message, int statusCode = 500)
    {
        return new(message, false, statusCode);
    }

    public static Result<T> Failure(List<string> messages, int statusCode = 500)
    {
        return new(messages, false, statusCode);
    }

    public static implicit operator Result<T>(T data)
    {
        return new(data);
    }
}
