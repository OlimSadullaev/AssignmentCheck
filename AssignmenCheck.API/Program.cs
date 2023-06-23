using AssignmenCheck.API.Extensions;
using AssignmentCheck.Data.Contexts;
using AssignmentCheck.Domains.Enums;
using AssignmentCheck.Service.Helpers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AssignmentCheckDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.admin),
        Enum.GetName(UserRole.teacher),
        Enum.GetName(UserRole.student)));

    options.AddPolicy("StudentPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.admin),
        Enum.GetName(UserRole.student)));

    options.AddPolicy("TeacherPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.teacher),
        Enum.GetName(UserRole.admin)));

    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.admin)));
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//Add Custom Services
//builder.Services.ConfigureJwt(builder.Configuration);
//builder.Services.AddSwaggerService();
//builder.Services.AddCustomServices();
//builder.Services.AddHttpContextAccessor();

// Convert Api url name to dash case
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(actionModelConvention: new RouteTokenTransformerConvention(
                                                            new ConfigureApiUrlName()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

//Set Helpers
EnvironmentHelper.WebRootPath = app.Services.GetRequiredService<IWebHostEnvironment>()?.WebRootPath;

if(app.Services.GetService<IHttpContextAccessor>() != null)
    HttpContextHelper.Accessor= app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
