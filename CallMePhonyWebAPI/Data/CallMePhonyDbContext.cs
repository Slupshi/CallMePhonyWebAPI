using CallMePhonyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CallMePhonyWebAPI.Data;

public class CallMePhonyDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public CallMePhonyDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString = _configuration.GetConnectionString("CallMePhonyDB");
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }



}