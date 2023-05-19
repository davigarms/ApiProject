using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
  options.AddPolicy("webclient",
    policy =>
    {
      policy.WithOrigins("https://localhost:7215")
        .AllowAnyHeader();
    }));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication("Bearer")
  .AddJwtBearer("Bearer", options =>
  {
    options.Authority = "https://localhost:5001";

    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateAudience = false
    };
  });

builder.Services.AddAuthorization(options =>
  options.AddPolicy("ApiScope", policy =>
  {
    policy.RequireAuthenticatedUser();
    policy.RequireClaim("scope", "api1");
  }));

var app = builder.Build();

app.UseRouting();
app.UseCors("webclient");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization("ApiScope");

app.Run();