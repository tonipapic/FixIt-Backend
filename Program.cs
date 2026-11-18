using FixIt_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("FixIt") ?? throw new InvalidOperationException("Connection string 'FixIt' not found.");



builder.Services.AddDbContextFactory<FixItContext>(opt => 
                    opt.UseSqlServer(connectionString)
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
               );

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<PrijavaRepository>();
builder.Services.AddScoped<LocalAuthService>();
builder.Services.AddTransient<JWTService>();
builder.Services.AddTransient<PrijavaService>();

var jwt = builder.Configuration.GetSection("JWT").Get<JWTModel>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt.Issuer,
        ValidAudience = jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwt.Key))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
