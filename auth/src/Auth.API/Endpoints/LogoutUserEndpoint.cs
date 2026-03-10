namespace Auth.API.Endpoints;

internal sealed class LogoutUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("logout", () =>
        {
            // TODO: Сейчас нет refreshToken, поэтому логаута только на клиенте (удаление токена)
            return Results.Ok(true);
        })
        .RequireAuthorization();
    }
}