using MasterPeace.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database connection
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionString"))); // Replace with actual connection string

// CORS setup
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin()    
            .AllowAnyMethod()    
            .AllowAnyHeader();   
    });
});

var shortPassphrase = "ShortPass"; 
var hashedKey = new HMACSHA256(Encoding.UTF8.GetBytes(shortPassphrase)).Key;

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,    // Validate the issuer (typically the domain of the API)
        ValidateAudience = true,  // Validate the audience
        ValidateLifetime = true,  // Ensure token hasn't expired
        ValidateIssuerSigningKey = true,  // Validate the signing key
        ValidIssuer = builder.Configuration["Jwt:Issuer"],   // Your issuer (from configuration)
        ValidAudience = builder.Configuration["Jwt:Audience"],  // Your audience (from configuration)
        IssuerSigningKey = new SymmetricSecurityKey(hashedKey)    // Hashed key used to sign JWT
    };
});

// Authorization setup
builder.Services.AddAuthorization();

var app = builder.Build();

// Swagger setup for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// CORS middleware
app.UseCors("AllowAllOrigins");

// Authentication and Authorization
app.UseAuthentication();  // This ensures that authentication middleware is used
app.UseAuthorization();   // This ensures that authorization middleware is used

// Map Controllers
app.MapControllers();

app.Run();
