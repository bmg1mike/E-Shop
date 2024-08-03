namespace Catalog.Api;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(string Name, List<string> Categories, string Description, string ImageFile, decimal Price);
public class GetProductByIdHandler(IDocumentSession documentSession,ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery,GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(product.Name, product.Categories, product.Description, product.ImageFile, product.Price);
    }
}
