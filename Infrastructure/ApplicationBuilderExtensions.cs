namespace VanillaArtStore.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using VanillaArtStore.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using VanillaArtStore.Data.Models;
    using System;
    using Microsoft.AspNetCore.Identity;

    using static WebConstants;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedServices.ServiceProvider;

            var data = serviceProvider.GetRequiredService<VanillaArtDbContext>();

            data.Database.Migrate();

            SeedCategories(data);
            SeedAdministrator(serviceProvider);

            return app;
        }

        private static void SeedCategories(VanillaArtDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category{Name = "Men" },
                new Category{Name = "Woman" },
                new Category{Name = "Home" },
                new Category{Name = "Gifts" },
                new Category{Name = "Antiques" },

            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminPass = "admin12";

                    var user = new User
                    {
                       FirstName = "Steliyan",
                       LastName = "Nachev",
                       UserName = "admin@gmail.com",
                       PhoneNumber = "0898460866",
                       Email = "admin@gmail.com",
                       HasDiscount=true
                    };

                    await userManager.CreateAsync(user, adminPass);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
