using System.Text;
using Backend.Data;
using Backend.Helpers;
using Backend.Interfaces;
using Backend.Middleware;
using Backend.Repositories;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    options.SuppressModelStateInvalidFilter = true
);

// DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClientOrigin", policyBuilder =>
        policyBuilder.WithOrigins(builder.Configuration["Client:Origin"])
                     .AllowAnyHeader()
                     .AllowAnyMethod());
});


// Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))

        };
    });

// Rate Limiter
builder.Services.AddGlobalRateLimiting();


// Authorization
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy(Roles.Admin, policy =>
        policy.RequireRole(Roles.Admin));

    options.AddPolicy(Roles.Moderator, policy =>
        policy.RequireRole(Roles.Admin, Roles.Moderator));

});

// Services
builder.Services.AddSingleton<TokenService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<IPredictionService, PredictionService>();
builder.Services.AddScoped<IPredictionRepository, PredictionRepository>();
builder.Services.AddScoped<IPredictionVoteService, PredictionVoteService>();
builder.Services.AddScoped<IPredictionVoteRepository, PredictionVoteRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseMiddleware<IpFilterMiddleware>(builder.Configuration["IpAllowList"]); // Need to change to dynamic persistence list
app.UseMiddleware<SecurityHeadersMiddleware>();


app.UseHttpsRedirection();

app.UseCors("AllowClientOrigin");
app.UseAuthentication();
// Fiddle with limit + throttle values + Graceful shutdown?
app.UseMiddleware<RequestThrottleMiddleware>(
    8,
    TimeSpan.FromMinutes(1),
    TimeSpan.FromMilliseconds(500));
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();
