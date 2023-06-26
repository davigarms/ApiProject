using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001";

        options.ClientId = "mvc";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.SaveTokens = true;
        
        options.ClaimActions.MapUniqueJsonKey("website", "website");
        // options.ClaimActions.MapUniqueJsonKey("website2", "website2");
        options.ClaimActions.MapUniqueJsonKey("address", "address");
        // options.ClaimActions.MapUniqueJsonKey("address2", "address2");
        options.ClaimActions.MapUniqueJsonKey("email_verified", "email_verified");
        
        options.Scope.Add("address");
        // options.Scope.Add("address2");
        // options.Scope.Add("website2");
        options.Scope.Add("email");
        
        options.Scope.Add("api1");
        
        options.GetClaimsFromUserInfoEndpoint = true;
    });
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
