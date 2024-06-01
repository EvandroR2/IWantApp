namespace IWantApp.Endpoints.Products;

public record ProductsRequest(string Name, Guid CategoryId, string Description, bool HasStock, bool Active);
