global using Microsoft.EntityFrameworkCore;
global using FastEndpoints;
global using FastEndpoints.Security;
global using FluentValidation;
global using Fastendpoint_api.Contracts.Requests;
global using Fastendpoint_api.Contracts.Responses;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder();

builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<Fastendpoint_api.Models.UserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IValidator<UpdateRequest>, UpdateValidator>();
builder.Services.AddAuthenticationJWTBearer(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthorization();
builder.Services.AddSwaggerDoc();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.UseHttpsRedirection();



app.Run();
