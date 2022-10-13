using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Models;

namespace MovieLibrary.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions 
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            MigrateDatabase(serviceProvider);
            SeedGenres(serviceProvider);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider
                .GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        private static void SeedGenres(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider
                .GetRequiredService<ApplicationDbContext>();


            if (dbContext.Genres.Any())
            {
                return;
            }

            dbContext.Genres.AddRange(new[]
            {
                new Genre {Name = "Comedy"},
                new Genre {Name = "Action"},
                new Genre {Name = "Adventure"},
                new Genre {Name = "Horror"},
                new Genre {Name = "Thriller"},
                new Genre {Name = "Drama"},
                new Genre {Name = "Science fiction"},
                new Genre {Name = "Crime"},
                new Genre {Name = "Romance"},
                new Genre {Name = "Narrative"},
                new Genre {Name = "Fantasy"},
                new Genre {Name = "Fiction"},
                new Genre {Name = "Musical"},
                new Genre {Name = "Documentary"},
                new Genre {Name = "Animation"},
                new Genre {Name = "History"},
                new Genre {Name = "Mystery"},
                new Genre {Name = "Science"},
                new Genre {Name = "Short"},
                new Genre {Name = "Biographical"},
                new Genre {Name = "Sports"},
                new Genre {Name = "Kids"}
            });

            dbContext.SaveChanges();
        }
    }
}