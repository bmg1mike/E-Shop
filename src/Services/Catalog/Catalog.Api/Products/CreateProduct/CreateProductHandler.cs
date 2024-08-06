using FluentValidation;

namespace Catalog.Api;

public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler(IDocumentSession documentSession) : ICommandHandler<CreateProductCommand, CreateProductResult>
{

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // var result = await validator.ValidateAsync(request, cancellationToken);
        // var errors = result.Errors.Select(x => x.ErrorMessage);
        // if(errors.Any())
        // {
        //     throw new ValidationException(errors.FirstOrDefault());
        // }

        var product = new Product
        {
            Name = request.Name,
            Categories = request.Categories,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price
        };

        documentSession.Store(product);
        await documentSession.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}

