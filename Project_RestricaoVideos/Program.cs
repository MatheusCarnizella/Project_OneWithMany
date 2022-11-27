using Microsoft.EntityFrameworkCore;
using Project_RestricaoVideos.Context;
using Project_RestricaoVideos.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapRestricaoEndPoint();
app.MapVideoEndPoint();

app.UseHttpsRedirection();

app.Run();
