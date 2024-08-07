﻿namespace Catalog.Api;

public record CreateProductRequest(string Name, List<string> Categories, string Description, string ImageFile, decimal Price);
public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (ISender _mediator, CreateProductRequest request) =>
        {
            var command = MapToCommand(request);
            var result = await _mediator.Send(command);
            // return new CreateProductResponse(result.Id);
            return Results.Created($"/products/{result.Id}", new CreateProductResponse(result.Id));
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new product")
        .WithDescription("Create a new product");
    }

    private CreateProductCommand MapToCommand(CreateProductRequest request)
    {
        return new CreateProductCommand(request.Name, request.Categories, request.Description, request.ImageFile, request.Price);
    }
}
