using Microsoft.AspNetCore.Identity;
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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


using (var scope = app.Services.CreateScope())
{
    var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = ["Admin", "User"];
    foreach(var role in roles)
        if (!await rolemanager.RoleExistsAsync(role))
        {
            await rolemanager.CreateAsync(new IdentityRole(role));
        }
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
