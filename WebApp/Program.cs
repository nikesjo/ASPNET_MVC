using Infrastructure.Helpers.Middlewares;
using Infrastructure.Services;
using WebApp.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.RegisterDbContexts(builder.Configuration);
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddSession(x =>
{
    x.IdleTimeout = TimeSpan.FromMinutes(20);
    x.Cookie.IsEssential = true;
    x.Cookie.HttpOnly = true;
});



var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseUserSessionValidation();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
