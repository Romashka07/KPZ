using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Додаємо налаштування CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // URL вашого React-додатку
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Додаємо автоматичне відображення для AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// Реєструємо AutoMapper і додаємо наш профіль
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Додаємо контекст бази даних
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Додаємо Swagger для документування API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// Додаємо контролери
builder.Services.AddControllers();

var app = builder.Build();

// Налаштування CORS перед іншими середовищами
app.UseCors("AllowReactApp");
// Конфігурація пайплайна HTTP запитів
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}


// Використовуємо HTTPS редирект та авторизацію
app.UseHttpsRedirection();
app.UseAuthorization();

// Маппінг контролерів
app.MapControllers();

// Запускаємо додаток
app.Run();
