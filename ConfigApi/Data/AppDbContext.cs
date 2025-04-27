using Microsoft.EntityFrameworkCore;
using ConfigApi.Models;

namespace ConfigApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Configuration> Configurations { get; set; }
}
