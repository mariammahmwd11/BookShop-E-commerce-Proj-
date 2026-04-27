
using BookSAW.business_logic.IServices;
using BookSAW.business_logic.Services;
using BookSAW.DataAccess.Data;
using BookSAW.DataAccess.Repositories.IRepositories;
using BookSAW.DataAccess.Repositories.Repository;
using BookSAW.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookSAW_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(14); // مدة الـ Remember Me
                options.SlidingExpiration = true;
                options.LoginPath = "/User/Account/Login";
                options.LogoutPath = "/User/Account/Logout";
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IImageServices, ImageServices>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
        builder.Services.AddControllersWithViews();
          

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            app.MapControllerRoute(
     name: "default",
     pattern: "{area=User}/{controller=HomePage}/{action=Index}/{id?}"
 );

            app.Run();
        }
    }
}
