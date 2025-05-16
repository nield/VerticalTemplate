using FastEndpoints.Swagger;
using VerticalTemplate.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.ConfigureLogging();

builder.ConfigureApiServices();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseFastEndpoints(options =>
{
    options.Versioning.Prefix = "v";
    options.Versioning.DefaultVersion = 1;
    options.Versioning.PrependToRoute = true;

    options.Endpoints.RoutePrefix = "api";

    options.Errors.UseProblemDetails();
}).UseSwaggerGen();

app.UseSwaggerUI();

app.UseHttpsRedirection();

await app.ApplyMigrations();

await app.RunAsync();

// Make the implicit Program class public so test projects can access it
public partial class Program
{
    protected Program()
    {

    }
}
