namespace Catalog.Api;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);
public class DeleteProductHandler(IDocumentSession documentSession) : ICommandHandler<DeleteProductCommand,DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }
        documentSession.Delete(product);
        await documentSession.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}
