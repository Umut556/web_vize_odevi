using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<IBolumRepository, BolumRepository>();
builder.Services.AddScoped<IDerslerRepository, DerslerRepository>();
builder.Services.AddScoped<IDonemlerRepository, DonemlerRepository>();
builder.Services.AddScoped<IFakulteRepository, FakulteRepository>();
builder.Services.AddScoped<INotlarRepository, NotlarRepository>();
builder.Services.AddScoped<IOgrenciRepository, OgrenciRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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

app.MapControllers();

app.Run();


