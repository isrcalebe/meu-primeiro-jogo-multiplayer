namespace Frutti.Server.Domain.Common;

public readonly struct Error
{
    public static readonly Error NONE = new(nameof(NONE), "");
    public static readonly Error NOT_FOUND = new(nameof(NOT_FOUND), "The requested item was not found on this server.");
    public static readonly Error UNEXPECTED = new(nameof(UNEXPECTED), "An unknown error occurred while processing your request.");

    public string Code { get; }

    public string Message { get; }

    internal Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
}

public static class ErrorExtensions
{
    public static Error WithCode(this Error self, string code) => new(code, self.Message);

    public static Error WithMessage(this Error self, string message) => new(self.Code, message);
}
