using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Models;

namespace MovieLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Character> Characters { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<MovieCharacter> MovieCharacters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieCharacter>()
                .HasKey(mch => new {mch.MovieId, mch.CharacterId});

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Creator)
                .WithMany(u => u.AddedMovies);

            base.OnModelCreating(modelBuilder);
        }
    }
}