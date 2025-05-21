using Frutti.Server.Application.Requests.Responses;
using Mediator;

namespace Frutti.Server.Application.Requests;

public record LoginRequest(
    string Username,
    string Password
) : IRequest<LoginResponse>;
