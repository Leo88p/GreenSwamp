using Lab3;
using Lab3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace Lab3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<SwampContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SwampContext")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<SwampContext>();
                context.Database.EnsureCreated();
                //DbInitializer.Initialize(context);
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/{0}");
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }

    [Route("subscribe")]
    public class SubscribeController : ControllerBase
    {
        [HttpPost]
        public IActionResult Subscribe(string data)
        {
            MailAddress from = new MailAddress("tertestmail@mail.ru", "Swamp");
            MailAddress to = new MailAddress(data);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Тест";
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("tertestmail@mail.ru", "xyL6MfwGiyKgxeLqnuph");
            smtp.EnableSsl = true;
            smtp.Send(m);
            return RedirectToPage("/Index");
        }
    }

    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}