
namespace Catalog.Api;

public record UpdateProductRequest(Product Product);
public record UpdateProductResponse(bool IsSuccess);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request,ISender sender) =>
        {
            var command = MapToCommand(request);
            var result = await sender.Send(command);
            return Results.Ok(new UpdateProductResponse(result.isSuccess));
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Update a product")
        .WithDescription("Update a product");
    }

    private UpdateProductCommand MapToCommand(UpdateProductRequest request) => new(request.Product);
}
