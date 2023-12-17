using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Metafar_API.Entities;
using Metafar_API.Services;
using Metafar_API.Services.Interfaces;
using Metafar_API.Data;
using Metafar_API.Repositories;
using Metafar_API.Repositories.Interfaces;
using Serilog;
using Metafar_API.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSql"));
});

builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();
builder.Services.AddScoped<ITarjetaService, TarjetaService>();
builder.Services.AddScoped<IExtraccionService, ExtraccionService>();
builder.Services.AddScoped<IOperacionService, OperacionService>();

builder.Services.AddScoped<CustomAuthorizeFilter>();

builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IAutorizacionRepository, AutorizacionRepository>();
builder.Services.AddScoped<ITarjetaRepository, TarjetaRepository>();
builder.Services.AddScoped<IOperacionRepository, OperacionRepository>();
builder.Services.AddScoped<IAuthorizationService, DefaultAuthorizationService>();
builder.Services.AddScoped<IAuthorizationHandlerProvider, DefaultAuthorizationHandlerProvider>();
builder.Services.AddScoped<IPolicyEvaluator, PolicyEvaluator>();

builder.Services.Configure<ConfiguracionPaginacion>(builder.Configuration.GetSection("Paginacion"));

var key = builder.Configuration.GetValue<string>("TokenSettings:TokenKey");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
