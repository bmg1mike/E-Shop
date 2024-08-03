namespace Catalog.Api;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product was not found.")
    {
    }
}
