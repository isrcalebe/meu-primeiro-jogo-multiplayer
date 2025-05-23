using System;

namespace Frutti.Shared.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Errors.NONE:
                throw new InvalidOperationException("A result cannot be successful and contain an error.");

            case false when error == Errors.NONE:
                throw new InvalidOperationException("A failure result must contain an error.");

            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public static Result Ok() => new(true, Errors.NONE);
}

public class Result<TValue> : Result
{
    private readonly TValue value;

    public TValue Value => IsSuccess ? value : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

    protected internal Result(TValue value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        this.value = value;
    }

    protected internal Result(bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        value = default!;
    }
}

public static class ResultExtensions
{
    public static Result<T> Ok<T>(T value) => new(value, true, Errors.NONE);

    public static Result<T> Error<T>(Error error) => new(false, Errors.NONE);
}
