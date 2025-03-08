using Carter;
using GithubActionTest.Models;
namespace GithubActionTest.Modules.ProductModule
{
    public class ProductModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            IEndpointRouteBuilder group = app.MapGroup("/api/products");

            group.MapGet("", () =>
            {
                List<Product> products =
                [
                    new() { Id = 1, Name = "Product 1", Price = 100 },
                    new() { Id = 2, Name = "Product 2", Price = 200 },
                    new() { Id = 3, Name = "Product 3", Price = 300 }
                ];
                return Results.Ok(products);
            }).Produces<List<Product>>();

            group.MapPost("", (Product product) =>
            {
                return Results.Created($"/api/products/{product.Id}", product);
            }).Produces<Product>();
        }
    }
}
