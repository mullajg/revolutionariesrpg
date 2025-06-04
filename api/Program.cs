using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using revolutionariesrpg.api;
using Microsoft.EntityFrameworkCore;
using revolutionariesrpg.api.Interfaces;

var builder = FunctionsApplication.CreateBuilder(args);
var cs = Environment.GetEnvironmentVariable("SqlConnectionString");

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(cs));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Build().Run();
