using EmailWebAPI.Data;
using EmailWebAPI.Repositories;
using EmailWebAPI.Services;
using EmailWebAPI.Settings;
using Microsoft.EntityFrameworkCore;

namespace EmailWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IMailService, MailService>();
            builder.Services.AddScoped<IMailRequestRepository, MailRequestRepository>();

            builder.Services.AddControllers();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
