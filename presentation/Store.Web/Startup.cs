using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store.Memory;
using System;
using Store.Messages;
using Store.Contractors;
using Store.Yandex.Kassa;
using Store.Web.Contractors;

namespace Store.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddDistributedMemoryCache(); // ���� ������� ����� ��������� � �������������� ������
            services.AddSession(options => 
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); // ����� ����� ������
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // �������� ���������� � ������������� �� ���������� JD
            });

            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<INotificationService, DebugNotificationService>();
            services.AddSingleton<IDeliveryService, PostamateDeliveryService>();
            services.AddSingleton<IPaymentService, CashPaymentService>();
            services.AddSingleton<IPaymentService, YandexKassaPaymentService>();
            services.AddSingleton<IWebContractorService, YandexKassaPaymentService>();
            services.AddSingleton<BookService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                // controller/action/{id(parametr)}
                // book
                // book/index
                // book/index/234 <- � ����������� book ����� �������� �������� index � ���������� 234

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // �� ��������� ���� ����� controller, �� �� ������ Home, � ���� �� ������ ��������, �� ��� index

                endpoints.MapAreaControllerRoute(
                    name: "yandex.kassa",
                    areaName: "YandexKassa",
                    pattern: "YandexKassa/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
