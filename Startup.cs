
namespace VanillaArtStore
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpsPolicy;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using VanillaArtStore.Data;
    using VanillaArtStore.Data.Models;
    using VanillaArtStore.Infrastructure;
    using VanillaArtStore.Services.Products;
    using VanillaArtStore.Services.ShoppingCart;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<VanillaArtDbContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options =>
                     {
                         options.SignIn.RequireConfirmedAccount = false;
                         options.Password.RequireDigit = false;
                         options.Password.RequireLowercase = false;
                         options.Password.RequireNonAlphanumeric = false;
                         options.Password.RequireUppercase = false;
                     })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<VanillaArtDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
            });

            services.AddControllersWithViews();

            services.AddTransient<IProductService,ProductService>();
            services.AddTransient<IShoppingCartService,ShoppingCartService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShoppingCartService.GetCart(sp));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseSession()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
