using BudgetBuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BudgetBuddy.Infrastructure.Data.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : DbContext(options)
{
    private readonly IConfiguration _configuration = configuration;
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId)
                  .HasDefaultValueSql("(UUID())")
                  .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.Username)
                  .IsUnique();

            entity.Property(e => e.Username)
                  .HasMaxLength(255)
                  .IsRequired();

            entity.Property(e => e.UserPassword)
                  .HasMaxLength(255)
                  .IsRequired();

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("(NOW())")
                  .ValueGeneratedOnAdd();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration["ConnectionStrings:DefaultConnection"];
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

}
