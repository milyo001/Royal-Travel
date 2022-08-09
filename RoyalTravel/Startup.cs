namespace RoyalTravel
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using RoyalTravel.Data;
    using RoyalTravel.Data.Models;
    using RoyalTravel.Services.Hotel;
    using RoyalTravel.Services.Room;
    using RoyalTravel.Services.Stays;
    using RoyalTravel.Services.User;

    // Project started 2019 with .NET 3.1 - without the minimal hosting model.
    // Apps migrating to .NET 6.0 don't need to use the new minimal hosting model.
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "RequireAdmin",
                    policy => policy.RequireRole("Administrator"));
            });
            services.AddRazorPages().AddMvcOptions(options =>
            {
                options.MaxModelValidationErrors = 50;
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    _ => "The field is required.");
            });

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IHotelService, HotelService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IStaysService, StaysService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios,
                // see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints
                .MapAreaControllerRoute(
                  name: "areas",
                  areaName: "areas",
                  pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });
        }
    }
}
