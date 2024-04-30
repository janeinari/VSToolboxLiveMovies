using Microsoft.EntityFrameworkCore;
using Movies.Data;
using MoviesRpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcReflection();


var connectionString = builder.Configuration.GetConnectionString("Sqlite");

builder.Services.AddDbContextPool<MovieContext>(options =>
{
  options.UseSqlite(connectionString);
});

var app = builder.Build();

app.UseCors(c =>
{
  c.AllowAnyOrigin();
  c.AllowAnyHeader();
  c.AllowAnyMethod();
});

app.UseRouting();

app.UseGrpcWeb();

app.MapGrpcService<MoviesImpl>().EnableGrpcWeb();
app.MapGrpcReflectionService();


app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
