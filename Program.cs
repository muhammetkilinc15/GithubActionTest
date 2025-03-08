using Carter;
using GithubActionTest.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Postgress");

    options.UseNpgsql(connectionString)
            .EnableDetailedErrors() // Detaylý hata mesajlarýný etkinleþtir
           .EnableSensitiveDataLogging(); // Veritabaný sorgularý ile ilgili hassas verileri logla
});


builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.DocumentTitle = "Github Action API";
    });
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.MapCarter();
app.Run();
