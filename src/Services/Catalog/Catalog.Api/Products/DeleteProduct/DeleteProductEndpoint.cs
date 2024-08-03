namespace Catalog.Api;

public record DeleteProductRequest(Guid id);
public record DeleteProductResponse(bool IsSuccess);
public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            var command = MapToCommand(id);
            var result = await sender.Send(command);
            return Results.Ok(new DeleteProductResponse(result.IsSuccess));
        });
    }

    private DeleteProductCommand MapToCommand(Guid id) => new DeleteProductCommand(id);
}

