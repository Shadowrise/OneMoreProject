using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using omp.Auth.DB;
using LoginRequest = omp.Auth.DTO.LoginRequest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.MapGet("/users", async (AppDbContext context, CancellationToken token) =>
    {
       var users = await context.Users.ToArrayAsync(token);
       return Results.Ok(users);
    })
    .WithName("GetUsers")
    .WithOpenApi();

app.MapPost("/login", async (LoginRequest request, AppDbContext context, CancellationToken token) =>
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, token);
        if (user == null)
        {
            return Results.BadRequest();
        }
        
        var jwt = Guid.NewGuid().ToString();
        return Results.Ok(jwt);
    })
    .WithName("Login")
    .WithOpenApi();

app.Run();