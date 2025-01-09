using FluentValidation.AspNetCore;
using FluentValidation;
using Mapster;
using MovieStoreB.BL;
using MovieStoreB.DL;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using MovieStoreB.Controllers;
using MovieStoreB.HealthChecks;
using MovieStoreB.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme:
        AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services
    .AddConfigurations(builder.Configuration)
    .AddDataDependencies()
    .AddBusinessDependencies();

builder.Services.AddMapster();

builder.Services.AddValidatorsFromAssemblyContaining<TestRequest>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//builder.Services.AddHealthChecks();

builder.Services.AddHealthChecks()
    .AddCheck<SampleHealthCheck>("Sample");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
