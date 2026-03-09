using Finance.Application.Todos.Commands.AddFavoriteCurrency;
using MediatR;

namespace Finance.API.Endpoints;

internal sealed class AddFavoriteCurrencyEndpoint : IEndpoint
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public Guid[] CurrencyIds { get; set; } = [];
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/finance/favorites", async (
            Request request,
            IMediator mediator) =>
        {
            var command = new AddFavoriteCurrencyCommand(request.UserId, request.CurrencyIds);
            
            await mediator.Send(command);

            return Results.Ok;
        });
    }
}
