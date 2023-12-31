using System.Net;
using System.Reflection;
using API.Application.Contracts;
using API.Application.Services;
using API.Authorization.Handlers;
using API.Domain.Contracts.Configuration;
using API.Domain.Contracts.Services;
using API.Domain.Entities;
using API.Domain.Repositories;
using API.Infrastructure.Database;
using API.Infrastructure.Repositories;
using API.Infrastructure.SensorApi.Services;
using API.Infrastructure.WeatherApi.Services;
using API.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies()
    .ApplicationCookie!.Configure(opt =>
    {
        opt.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = ctx =>
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = ctx =>
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden;
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
builder.Services.AddFluentValidationRulesToSwagger();

// Add AutoMapper
builder.Services.AddAutoMapper(
    Assembly.GetExecutingAssembly()
        .GetReferencedAssemblies()
        .Select(Assembly.Load)
);

// Enable the HTTP Client
builder.Services.AddHttpClient();

// Register configuration
builder.Services.Configure<WeatherApiSettings>(builder.Configuration.GetSection("WeatherAPI"));
builder.Services.Configure<SensorApiSettings>(builder.Configuration.GetSection("SensorAPI"));

// Register application services
builder.Services.AddScoped<IWeatherForecastService, ForecastWeatherApiService>();
builder.Services.AddScoped<ICurrentWeatherService, CurrentWeatherApiService>();
builder.Services.AddScoped<IWeatherHistoryService, HistoryWeatherApiService>();
builder.Services.AddScoped<IPinnedCityService, PinnedCityService>();
builder.Services.AddScoped<IPinnedCityWeatherService, PinnedCityWeatherService>();
builder.Services.AddScoped<ISensorsService, SensorsService>();
builder.Services.AddScoped<IPinnedSensorService, PinnedSensorService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IAuthSessionService, AuthSessionService>();
builder.Services.AddScoped<ISignInService, SignInService>();
builder.Services.AddScoped<IUserService, UserService>();

// Register repositories
builder.Services.AddScoped<IPinnedCityRepository, PinnedCityRepository>();
builder.Services.AddScoped<IPinnedSensorRepository, PinnedSensorRepository>();

// Register authorization handlers
builder.Services.AddScoped<IAuthorizationHandler, PinnedCityCrudHandler>();
builder.Services.AddScoped<IAuthorizationHandler, PinnedSensorCrudHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policyBuilder =>
    {
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
}
else
{
    app.UseHttpsRedirection();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();