using EmailWebAPI.Services;
using EmailWebAPI.Settings;

namespace EmailWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IMailService, MailService>();

            builder.Services.AddControllers();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
