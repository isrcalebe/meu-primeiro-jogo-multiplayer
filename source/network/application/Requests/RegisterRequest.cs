using Frutti.Server.Application.Requests.Responses;
using Mediator;

namespace Frutti.Server.Application.Requests;

public record RegisterRequest(
    string Username,
    string Password
) : IRequest<RegisterResponse>;
