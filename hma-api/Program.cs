using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using hma_api.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using hma_api.Application.Interfaces;
using hma_api.Application.Services;
using hma_api.Domain.Interfaces;
using hma_api.Infraestructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Jwt configuration
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
   options.TokenValidationParameters = new TokenValidationParameters
   {
     ValidateIssuer = true,
     ValidateAudience = true,
     ValidateLifetime = true,
     ValidateIssuerSigningKey = true,
     ValidIssuer = jwtIssuer,
     ValidAudience = jwtIssuer,
     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
   };
 });

// Add services to the container.

builder.Services.AddControllers();

//Database configuration
builder.Services.AddDbContext<MyDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//Inject repositories
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));

//Inject services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

  // Security configuration for the JWT scheme
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please type the token like: 'Bearer {token}'",
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// Middleware
app.UseHttpsRedirection();

// Add Authentication
app.UseAuthentication(); // Importante para JWT
app.UseAuthorization();

// Si necesitas CORS
// app.UseCors("AllowAll");

app.MapControllers();

app.Run();