using MentorMVCTemplate.DataService;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MentorMVCTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure authentication using cookies
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/authentication/login";
                    options.LogoutPath = "/authentication/logout";
                    options.AccessDeniedPath = "/authentication/access-denied"; // authenticated but roles not matched
                    options.Cookie.Name = "auth_token";
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(30); // Set cookie expiration to 30 minutes
                    options.SlidingExpiration = true;
                });
            builder.Services.AddAuthorization();
            
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

            app.UseAuthentication(); // Enable authentication middleware
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
