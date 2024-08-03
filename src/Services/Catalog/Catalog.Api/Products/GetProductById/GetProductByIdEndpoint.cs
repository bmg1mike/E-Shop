
namespace Catalog.Api;

public record GetProductByIdResponse(string Name, string Description, string ImageFile, decimal Price);
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (ISender _mediator, Guid id) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Results.Ok(MapToResponse(result));
        }).WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get a product by id")
        .WithDescription("Get a product by id");
    }

    public GetProductByIdResponse MapToResponse(GetProductByIdResult result)
    {
        return new GetProductByIdResponse(result.Name, result.Description, result.ImageFile, result.Price);
    }
}
