using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ������ ������������ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // URL ������ React-�������
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// ������ ����������� ����������� ��� AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// �������� AutoMapper � ������ ��� �������
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ������ �������� ���� �����
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// ������ Swagger ��� �������������� API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// ������ ����������
builder.Services.AddControllers();

var app = builder.Build();

// ������������ CORS ����� ������ ������������
app.UseCors("AllowReactApp");
// ������������ ��������� HTTP ������
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}


// ������������� HTTPS �������� �� �����������
app.UseHttpsRedirection();
app.UseAuthorization();

// ������ ����������
app.MapControllers();

// ��������� �������
app.Run();
