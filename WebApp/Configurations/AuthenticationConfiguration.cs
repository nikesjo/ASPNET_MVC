using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace WebApp.Configurations;

public static class AuthenticationConfiguration
{
    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<UserEntity>(x =>
        {
            x.User.RequireUniqueEmail = true;
            x.SignIn.RequireConfirmedAccount = false;
            x.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<DataContext>();

        services.ConfigureApplicationCookie(x =>
        {
            x.LoginPath = "/signin";
            x.LogoutPath = "/signout";
            x.AccessDeniedPath = "/denied";

            x.Cookie.HttpOnly = true;
            x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            x.SlidingExpiration = true;
        });

        services.AddAuthentication().AddFacebook(x =>
        {
            x.AppId = "1477856659826934";
            x.AppSecret = "0ea4df4b97209bf7538c3e7f1bf12567";
            x.Fields.Add("first_name");
            x.Fields.Add("last_name");
        });

        services.AddAuthentication().AddGoogle(x =>
        {
            x.ClientId = "169070126063-bd5vkl5e8c4pd4a8mud5rvfiic10uv6p.apps.googleusercontent.com";
            x.ClientSecret = "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";
        });
    }
}
