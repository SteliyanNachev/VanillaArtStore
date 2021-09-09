namespace VanillaArtStore.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using VanillaArtStore.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using VanillaArtStore.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<VanillaArtDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

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
    }
}
