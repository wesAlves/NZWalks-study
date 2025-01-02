using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Injecting the db context class
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NZWalksConnectionString")));


builder.Services.AddScoped<IRegionRepository, PSQLRgionRepository>();
builder.Services.AddScoped<IWalkRepository, PSQLWalkRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();