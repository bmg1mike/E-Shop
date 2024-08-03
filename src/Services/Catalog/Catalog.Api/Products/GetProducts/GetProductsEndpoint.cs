
namespace Catalog.Api;

public record GetProductsRequest();
public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender _mediator) =>
        {
            var result = await _mediator.Send(new GetProductsQuery());
            var products = MapToResponse(result.Products);
            return Results.Ok(products);
        }).WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get all products")
        .WithDescription("Get all products");
    }

    public GetProductsResponse MapToResponse(IEnumerable<Product> products)
    {
        return new GetProductsResponse(products);
    }
}
