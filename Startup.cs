using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuanLiQuanAn.Data;
using QuanLiQuanAn.Utils;
using QuanLiQuanAn.Controllers;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddRazorPages(); // Thêm hỗ trợ Razor Pages

        services.AddTransient<TaiKhoanController>(); // Đăng ký TaiKhoanController
        services.AddSingleton<DatabaseHelper>(); // Đăng ký DatabaseHelper

        // Đăng ký các dịch vụ khác nếu cần
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages(); // Thêm Razor Pages vào các endpoint
            endpoints.MapControllers();
        });
    }
}
