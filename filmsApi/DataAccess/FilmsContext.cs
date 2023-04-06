using filmsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace filmsApi.DataAccess
{
    public partial class FilmsContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public FilmsContext(IConfiguration configuration) => _configuration = configuration;

        public virtual DbSet<Movie>? Movies { get; set; }
        public virtual DbSet<MovieRating>? MovieRatings { get; set; }
        public virtual DbSet<Actor>? Actors { get; set; }
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("filmsdb"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");
                entity.Property(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.Released);
                entity.Property(e => e.Length);
                entity.HasMany(e => e.Actors);
                entity.HasOne(e => e.Rating);
            })
            .Entity<MovieRating>(entity =>
            {
                entity.ToTable("MovieRating");
                entity.Property(e => e.Id);
                entity.Property(e => e.Rating)
                    .HasConversion(e => e.ToString(), e => (ParentalGuide)Enum.Parse(typeof(ParentalGuide), e, true));
            })
            .Entity<Actor>(entity =>
            {                
                entity.ToTable("Actor").Property(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.YearOfBirth);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}