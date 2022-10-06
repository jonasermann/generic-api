using Microsoft.EntityFrameworkCore;
using GenericApi.Data;
using GenericApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IGenericRepository, GenericRepository>();

// Add services to the container.
builder.Services.AddControllers();

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<GenericAppContext>(options =>
    options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
