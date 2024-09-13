using backend.Data;
using backend.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddScoped<IRepository<Blog>, SqlRepository<Blog>>();
builder.Services.AddScoped<IRepository<Category>, SqlRepository<Category>>();
builder.Services.AddScoped<IRepository<User>, SqlRepository<User>>();

builder.Services.AddAuthentication().AddBearerToken();
builder.Services.AddAuthentication();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Cambia a tu URL de frontend
               .AllowAnyHeader() // Permite cualquier encabezado, como 'Content-Type'
               .AllowAnyMethod(); // Permite cualquier método HTTP
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

// Aplicar la política de CORS antes de la autorización
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
