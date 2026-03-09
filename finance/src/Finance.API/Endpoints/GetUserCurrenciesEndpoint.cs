using System.Security.Claims;
using Finance.Application.Todos.Queries.GetUserCurrencies;
using MediatR;

namespace Finance.API.Endpoints;

internal sealed class GetUserCurrenciesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/finance/{userId}/rates", async (
            Guid userId,
            IMediator mediator) =>
        {
            var query = new GetUserCurrenciesQuery(userId);

            var result = await mediator.Send(query);

            return Results.Ok(result);
        });
    }
}

internal sealed class TestEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/test2", (ClaimsPrincipal user, IMediator mediator) =>
            {
                var userId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            
                return 123;
            })
            .RequireAuthorization();;
    }
}
