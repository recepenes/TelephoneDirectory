using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.DataAccessLayer.Entities;
using TelephoneDirectory.DataAccessLayer.Repository;
using TelephoneDirectory.DataAccessLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<Context>(options => options.UseNpgsql(configuration.GetConnectionString("defaultConnection")));

//register our services
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddTransient<IContactRepository, ContactRepository>();

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
