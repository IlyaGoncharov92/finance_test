using System.Security.Claims;
using Finance.Application.Todos.Queries;
using MediatR;

namespace Finance.API.Endpoints;

internal sealed class GetFavoriteCurrenciesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("favorites", async (
            ClaimsPrincipal user,
            IMediator mediator) =>
        {
            var userId = user.GetUserId();
            
            var query = new GetFavoriteCurrenciesQuery(userId);

            var result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .RequireAuthorization();
    }
}
