using Carter;
using GithubActionTest.Context;
using GithubActionTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GithubActionTest.Modules.ProductModule
{
    public class ProductModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            IEndpointRouteBuilder group = app.MapGroup("/api/products");

            group.MapGet("", async ([FromServices]ApplicationDbContext context) =>
            {
                var products = await context.Products.ToListAsync();
                return Results.Ok(products);
            }).Produces<List<Product>>();

            group.MapPost("", async ([FromServices]ApplicationDbContext context,[FromBody]Product product) =>
            {
                await context.Products.AddAsync(product);

                await context.SaveChangesAsync();
                return Results.Created($"/api/products/{product.Id}", product);
            }).Produces<Product>();
        }
    }
}
