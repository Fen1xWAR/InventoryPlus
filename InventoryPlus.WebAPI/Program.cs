using System.Text;
using InventoryPlus.Infrastructure;
using InventoryPlus.Infrastructure.Interfaces;
using InventoryPlus.Infrastructure.Repositories;
using InventoryPlus.WebAPI.Controllers.Service;
using InventoryPlus.WebAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

// Создание и настройка приложения
var builder = WebApplication.CreateBuilder(args);

// Настройка системы логирования
// Используется Serilog для более детального логирования
// Настройки включают:
// - Минимальный уровень логирования: Information
// - Для Microsoft.* - Warning
// - Для EF Core команд - Information
// - Вывод в консоль с форматированием времени
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information)
    .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

// Регистрация базовых сервисов ASP.NET Core
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Настройка Swagger/OpenAPI
// Включает:
// - Документацию API
// - Настройку авторизации через JWT токен
// - Описание безопасности
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "InventoryPlus", 
        Version = "v1",
        Description = "API для системы инвентаризации",
        Contact = new OpenApiContact
        {
            Name = "Prohortsev Andrew",
            Email = "proВВanvl@gmail.com"
        }
    });
    
    // Настройка авторизации через Bearer token в Swagger UI
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Введите JWT токен авторизации в формате: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // Требование авторизации для всех эндпоинтов
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Настройка контекста базы данных
// Используется PostgreSQL с настройкой логирования через Serilog
builder.Services.AddDbContext<InventoryContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("InventoryConnection"));
    options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddSerilog()));
});

// Регистрация сервисов и репозиториев
// Используется Scoped lifetime для обеспечения изоляции данных между запросами
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<ICabinetRepository, CabinetRepository>();
builder.Services.AddScoped<IConsumableCategoryRepository, ConsumableCategoryRepository>();
builder.Services.AddScoped<IConsumableModelRepository, ConsumableModelRepository>();
builder.Services.AddScoped<IConsumableRepository, ConsumableRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEquipmentConsumableRepository, EquipmentConsumableRepository>();
builder.Services.AddScoped<IEquipmentInstanceRepository, EquipmentInstanceRepository>();
builder.Services.AddScoped<IEquipmentModelRepository, EquipmentModelRepository>();
builder.Services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();
builder.Services.AddScoped<IOperationHistoryRepository, OperationHistoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Настройка аутентификации JWT
// Включает:
// - Настройку схемы аутентификации
// - Валидацию токена
// - Проверку подписи
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

// Сборка приложения
var app = builder.Build();

// Настройка конвейера обработки HTTP-запросов
if (app.Environment.IsDevelopment())
{
    // Включение Swagger UI только в режиме разработки
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "InventoryPlus API v1");
        c.RoutePrefix = "swagger";
    });
}

// Настройка middleware
app.UseHttpsRedirection(); // Перенаправление HTTP на HTTPS
app.UseAuthentication(); // Аутентификация
app.UseAuthorization(); // Авторизация
app.UseMiddleware<ExceptionHandlingMiddleware>(); // Обработка исключений
app.UseMiddleware<RequestLoggingMiddleware>(); // Логирование запросов

app.MapControllers();

// Запуск приложения
app.Run();