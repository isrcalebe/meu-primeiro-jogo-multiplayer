using Mediator;

namespace Frutti.Server.Application.Requests.Responses;

public record RegisterResponse(
    bool Success,
    string Message
) : IRequest<RegisterResponse>;
