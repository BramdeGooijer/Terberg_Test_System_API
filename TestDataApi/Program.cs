using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestDataApi.Interfaces.Authentication;
using TestDataApi.Persistence;
using TestDataApi.Persistence.Configurations;
using TestDataApi.Services.Authentication;
using TestDataApi.Services.Reports;
using AuthenticationService = TestDataApi.Services.Authentication.AuthenticationService;
using IAuthenticationService = TestDataApi.Services.Authentication.IAuthenticationService;

var DashboardOrigin = "_dashboardOrigin";

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: DashboardOrigin,
            policy =>
            {
                policy.WithOrigins("http://localhost:3000", "https://dashboard.wkong.nl")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });
    
    builder.Services.AddControllers();
    
    // Services
    builder.Services.AddScoped<IReportService, ReportService>();
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    
    // Jwt generation
    var jwtSettings = new JwtSettings();
    builder.Configuration.Bind(JwtSettings.SectionName, jwtSettings);
    builder.Services.AddSingleton(Options.Create(jwtSettings));
    
    builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    
    // Authentication
    builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret))
        });

    // Database connection
    var databaseSettings = new DatabaseSettings();
    builder.Configuration.Bind(DatabaseSettings.SectionName, databaseSettings);
    builder.Services.AddSingleton(Options.Create(databaseSettings));

    Console.WriteLine(string.Format("Connecting to DB: Host={0};Username={1};Password={2};Database={3}",
        databaseSettings.Host,
        databaseSettings.Username,
        databaseSettings.Password,
        databaseSettings.Database));
    
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(string.Format("Host={0};Username={1};Password={2};Database={3}",
            databaseSettings.Host,
            databaseSettings.Username,
            databaseSettings.Password,
            databaseSettings.Database))
    );
}

var app = builder.Build();
{
    app.UseHttpsRedirection();

    app.UseCors(DashboardOrigin);
    
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
