
namespace Catalog.Api;

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (ISender _mediator, string category) =>
        {
            var query = new GetProductsByCategoryQuery(category);
            var result = await _mediator.Send(query);
            return Results.Ok(new GetProductsByCategoryResponse(result.Products));
        }).WithName("GetProductsByCategory")
        .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get products by category")
        .WithDescription("Get products by category");
    }
    
}
