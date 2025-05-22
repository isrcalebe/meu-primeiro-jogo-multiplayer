namespace Frutti.Server.Domain.Common;

public struct Result<T>
{
    public T? Value { get; }

    public Error? Error { get; }

    public bool IsSuccess => Error is not null;

    internal Result(T value)
    {
        Value = value;
        Error = null;
    }

    internal Result(Error error)
    {
        Error = error;
        Value = default;
    }
}

public static class ResultExtensions
{
    public static Result<T> Ok<T>(T value) => new(value);

    public static Result<T> Error<T>(Error error) => new(error);
}
