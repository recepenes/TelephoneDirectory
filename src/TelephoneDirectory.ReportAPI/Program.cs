using EasyNetQ;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TelephoneDirectory.DataAccessLayer.Entities;
using TelephoneDirectory.ReportAPI;
using TelephoneDirectory.ReportAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Dependency Injection
ConfigurationManager configuration = builder.Configuration;

// Register db context
builder.Services.AddDbContext<Context>(options => options.UseNpgsql(configuration.GetConnectionString("defaultConnection")));


// Register rabbitmq
builder.Services.RegisterEasyNetQ(
    builder.Configuration.GetConnectionString("RABBITMQ_CONNECTION"),
    x => x.EnableSystemTextJson()
);
// Register mapster
var config = MappingConfiguration.Generate();

builder.Services.AddSingleton(config);
builder.Services.AddSingleton<IMapper, ServiceMapper>();

// Register our services
builder.Services.AddScoped<IReportService, ReportService>();

#endregion

var app = builder.Build();

// Expose file path
var reportPath = app.Configuration.GetValue<string>("ReportPath");
if (reportPath != null)
{
    if (!Directory.Exists(reportPath)) Directory.CreateDirectory(reportPath);

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(reportPath),
        RequestPath = "/Reports"
    });
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();