using API.Data;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configure API URLs from environment variables if provided
var apiUrls = Environment.GetEnvironmentVariable("API_URLS");
if (!string.IsNullOrEmpty(apiUrls))
{
    builder.WebHost.UseUrls(apiUrls.Split(';'));
}

// Add services to the container
builder.Services.AddControllers();

// Add HttpContextAccessor for HATEOAS links
builder.Services.AddHttpContextAccessor();

// Configure EF Core with SQL Server - For development, use in-memory database
// In production, this would use Azure SQL as per ADR-005
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("SkillManagementDb"));
}
else
{
    // In production, connect to Azure SQL
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// Register repositories
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<ISkillsRepository, SkillsRepository>();
builder.Services.AddScoped<IPersonSkillsRepository, PersonSkillsRepository>();
builder.Services.AddScoped<ISkillVerificationsRepository, SkillVerificationsRepository>();

// Register mapping service
builder.Services.AddScoped<MappingService>();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { 
        Title = "Skill Management API", 
        Version = "v1",
        Description = "API for managing people's skills with proficiency levels and verifications"
    });
});

// Add CORS support for the frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicies.AllowFrontend, policy =>
    {
        // Get allowed origins from configuration with fallback to defaults
        var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? 
            new[] { "http://localhost:3000" };
            
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Initialize the database with seed data
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();
app.UseCors(CorsPolicies.AllowFrontend);
app.UseAuthorization();
app.MapControllers();

app.Run();
