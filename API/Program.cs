using System.Reflection;
using API.Application.Contracts;
using API.Domain.Entities;
using API.Domain.Contracts.Configuration;
using API.Domain.Contracts.Services;
using API.Application.Services;
using API.Infrastructure.Database;
using API.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies()
    .ApplicationCookie!.Configure(opt =>
    {
        opt.Events = new CookieAuthenticationEvents()
        {
            OnRedirectToLogin = ctx =>
            {
                ctx.Response.StatusCode = 401;
                return Task.CompletedTask;
            }
        };
    }); 
builder.Services.AddAuthorizationBuilder();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionString"]);
});
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblies(
    Assembly.GetExecutingAssembly()
        .GetReferencedAssemblies()
        .Select(Assembly.Load)
);

// Enable the HTTP Client
builder.Services.AddHttpClient();

// Register application services
builder.Services.Configure<WeatherApiSettings>(builder.Configuration.GetSection("WeatherAPI"));
builder.Services.AddScoped<IForecastWeatherApiService, ForecastWeatherApiService>();
builder.Services.AddScoped<ICurrentWeatherApiService, CurrentWeatherApiService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IAuthSessionService, AuthSessionService>();
builder.Services.AddScoped<ISignInService, SignInService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policyBuilder => {
        policyBuilder
            .WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200",
                "http://127.0.0.1:4200",
                "https://127.0.0.1:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
} else {    
    app.UseHttpsRedirection();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();