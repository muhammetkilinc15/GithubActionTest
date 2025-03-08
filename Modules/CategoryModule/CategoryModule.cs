using Carter;
using GithubActionTest.Context;
using GithubActionTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GithubActionTest.Modules.CategoryModule
{
    public class CategoryModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            IEndpointRouteBuilder group = app.MapGroup("/api/categories");

            group.MapGet("", async ([FromServices] ApplicationDbContext context,CancellationToken cancellationToken) =>
            {
               var categories = await context.Categories.ToListAsync(cancellationToken);
                return Results.Ok(categories);
            }).Produces<List<Category>>();

            group.MapPost("", async ([FromBody] Category category, [FromServices] ApplicationDbContext context, CancellationToken cancellationToken) =>
            {
                await context.Categories.AddAsync(category, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return Results.Created($"/api/categories/{category.Id}", category);
            }).Produces<Category>();
        }
    }
}
