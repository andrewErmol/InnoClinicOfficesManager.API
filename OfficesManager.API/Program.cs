//using Microsoft.AspNetCore.HttpOverrides;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using OfficesManager.API.Extensions;
//using OfficesManager.Domain;
//using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();

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

var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.ConfigureSwagger();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.ConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();