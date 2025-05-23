using MessagePack;

namespace Frutti.Shared.Common;

[MessagePackObject]
public record Error
{
    [Key(0)]
    public string Code { get; }

    [Key(1)]
    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
}
