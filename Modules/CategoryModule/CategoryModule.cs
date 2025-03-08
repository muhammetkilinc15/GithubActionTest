using Carter;
using GithubActionTest.Models;

namespace GithubActionTest.Modules.CategoryModule
{
    public class CategoryModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            IEndpointRouteBuilder group = app.MapGroup("/api/categories");

            group.MapGet("", () =>
            {
                List<Category> categories =
                [
                    new() { Id = 1, Name = "Category 1" },
                    new() { Id = 2, Name = "Category 2" },
                    new() { Id = 3, Name = "Category 3" }
                ];
                return Results.Ok(categories);
            }).Produces<List<Category>>();
        }
    }
}
