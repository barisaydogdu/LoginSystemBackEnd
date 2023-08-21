using LoginSystemBackEnd.Data;
using LoginSystemBackEnd.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace LoginSystemBackEnd
{
    public static class Startup
    {
       
        public static WebApplication InitializeApp(string[]args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;

        }
        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            }
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<IdentityContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("LoginSystemConnectionString")));
            builder.Services.AddSingleton<DbContextProvider>();
            builder.Services.AddSingleton<IUserService, UserService>();
        }
        private static void Configure(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");

            app.Run();

        }

    }
    }

