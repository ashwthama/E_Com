using E_Commerce.Domain.Context;
using E_Commerce.Domain.Models.Chat;
using E_Commerce.Repository.CartRepository;
using E_Commerce.Repository.DiscountRepository;
using E_Commerce.Repository.ProductRepository;
using E_Commerce.Repository.SaleSRepository;
using E_Commerce.Repository.UserRepo;
using E_Commerce.Services;
using E_Commerce.Services.AccountServices;
using E_Commerce.Services.CartSevices;
using E_Commerce.Services.DiscountServices;
using E_Commerce.Services.Slaeservice;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();

//Migration
builder.Services.AddDbContext<UserContext>(options =>
options.
UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Fluent APi
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddTransient<IProductServices, ProductServices>();

builder.Services.AddScoped<ISalesRepo, SalesRepo>();
builder.Services.AddTransient<ISalesServices, SalesServices>();

builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddTransient<ICartService, CartService>();

builder.Services.AddScoped<IDiscountRepo, DiscountRepo>();
builder.Services.AddTransient<IDIscountService, DIscountService>();

//twilio services

Environment.SetEnvironmentVariable("Sid", "AC14210b067669f26e0d758a3f2847cc2a");
Environment.SetEnvironmentVariable("Token", "6bb273c823fe0d9495d7d5b9ddcc4b7c");

// JWT Configuration

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR(option =>
{
    option.EnableDetailedErrors = true;
});

//TOKEN KEY
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseAuthentication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chatHub");

app.Run();