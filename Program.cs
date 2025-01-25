using Cake_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Cake_Shop.Areas.Identity.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();


        builder.Services.AddDbContext<ApplicationDbContext>(option =>

        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));

        });

        //ﬂ–« « ’· »«·œ« « »Ì”
        builder.Services.AddDbContext<DashboardContext>(option =>

        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));

        });


        builder.Services.AddDefaultIdentity<CakeShopUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DashboardContext>();
        builder.Services.AddRazorPages();

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

        app.MapStaticAssets();

        app.MapRazorPages();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Dashboard}/{action=Index}/{id?}")
            .WithStaticAssets();


        app.Run();
    }
}
