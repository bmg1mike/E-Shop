namespace Catalog.Api;

public record GetProductsByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);
public class GetProductsByCategoryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>()
             .Where(p => p.Categories.Contains(request.category))
             .ToListAsync(cancellationToken);

        return new GetProductsByCategoryResult(products);
    }
}
