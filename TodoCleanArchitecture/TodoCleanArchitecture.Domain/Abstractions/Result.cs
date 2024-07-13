using System.Text.Json.Serialization;

namespace TodoCleanArchitecture.Domain.Abstractions;
public sealed class Result<T>
{
    private Result(T data)
    {
        IsSuccessful = true;
        StatusCode = 200;
        Data = data;
    }

    private Result(int statusCode, string errorMessage)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
    public T? Data { get; private set; }
    public string? ErrorMessage { get; private set; }
    public bool IsSuccessful { get; private set; }
    [JsonIgnore]
    public int StatusCode { get; private set; }

    public static Result<T> Succeed(T data)
    {
        return new Result<T>(data);
    }

    public static Result<T> Failure(int statusCode, string errorMessage)
    {
        return new Result<T>(statusCode, errorMessage);
    }

    public static implicit operator Result<T>(T data)
    {
        return Result<T>.Succeed(data);
    }
}
