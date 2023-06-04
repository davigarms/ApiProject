using Services.Client.Implementation;
using StyledRazor.Core.Browser;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<EventsService>();
builder.Services.AddScoped<IdentityService>();
builder.Services.AddScoped<BrowserService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
