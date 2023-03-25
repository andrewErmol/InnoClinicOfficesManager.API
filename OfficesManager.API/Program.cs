using Microsoft.IdentityModel.Tokens;
using OfficesManager.API.Extensions;
using OfficesManager.Database;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.Enrich.FromLogContext()
        .ReadFrom.Configuration(context.Configuration);
});


// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureCors();

builder.Services.AddHttpClient();

builder.Services.ConfigureSqlContext(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication("Bearer")
         .AddJwtBearer("Bearer", options =>
         {
             options.Authority = "https://localhost:7130";

             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateAudience = false
             };
         });

builder.Services.ConfigureSwagger();

builder.Services.ConfigureServices();
builder.Services.ConfigureMassTransit(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
app.MigrateDatabase();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();