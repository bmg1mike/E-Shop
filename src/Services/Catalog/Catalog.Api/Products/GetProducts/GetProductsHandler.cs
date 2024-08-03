using System.Collections.Generic;

namespace Catalog.Api;

public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);
public class GetProductsHandler(IDocumentSession documentSession, ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Querying for products");
        var products = await documentSession.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}
