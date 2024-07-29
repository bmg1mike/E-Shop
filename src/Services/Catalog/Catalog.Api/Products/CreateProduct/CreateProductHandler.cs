using BuildingBlocks;

namespace Catalog.Api;

public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    // private readonly IProductRepository _productRepository;

    // public CreateProductCommandHandler(IProductRepository productRepository)
    // {
    //     _productRepository = productRepository;
    // }

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Categories = request.Categories,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price
        };

        // await _productRepository.AddAsync(product);

        return new CreateProductResult(product.Id);
    }
}

