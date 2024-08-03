
namespace Catalog.Api;

public record UpdateProductCommand(Product Product) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool isSuccess);
public class UpdateProductHandler(IDocumentSession documentSession) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(request.Product.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }
        product.Name = request.Product.Name ?? product.Name;
        product.Description = request.Product.Description ?? product.Description;
        product.Price = request.Product.Price;
        product.Description = request.Product.Description ?? product.Description;
        product.Categories = request.Product.Categories ?? product.Categories;
        product.ImageFile = request.Product.ImageFile ?? product.ImageFile;
        documentSession.Update(product);
        await documentSession.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}
