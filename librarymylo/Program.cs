using librarymylo_BLL.Interfaces.Repositories;
using librarymylo_BLL.Interfaces.Services;
using librarymylo_BLL.Services;
using librarymylo_DAL.Data;
using librarymylo_DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.Hosting;

namespace librarymylo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(10, 4, 22)),
                    mySqlOptions => mySqlOptions.MigrationsAssembly("librarymylo.WebApi")
                ));

            builder.Services.AddScoped<ApplicationDbContext>();

            builder.Services.AddScoped<ILibraryItemService, LibraryItemService>();
            builder.Services.AddScoped<ILibraryItemRepository, LibraryItemRepository>();

            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Google:ClientSecret"];
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true) // Allow any origin
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            app.UseCors("AllowSpecificOrigin");

/*            app.UseCors(policy => policy.AllowAnyHeader()
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials())*/;

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images"
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllers();

            app.Run();
        }
    }
}