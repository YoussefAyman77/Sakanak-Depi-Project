using Microsoft.EntityFrameworkCore;
using Sakanak.BLL.Interfaces;
using Sakanak.BLL.Services;
using Sakanak.DAL.Data;
using Sakanak.DAL.UnitOfWork;

namespace Sakanak.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // DAL dependencies
            builder.Services.AddDbContext<SakanakDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // BLL services
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IContractService, ContractService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ILandlordService, LandlordService>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
