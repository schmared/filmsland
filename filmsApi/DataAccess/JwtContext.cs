using filmsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace filmsApi.DataAccess
{
    public partial class JwtContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public JwtContext(IConfiguration configuration) => _configuration = configuration;

        public virtual DbSet<UserInfo>? UserInfos { get; set; }
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("jwtdb"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("UserInfo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.DisplayName).HasMaxLength(60).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Password).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.CreatedDate).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}