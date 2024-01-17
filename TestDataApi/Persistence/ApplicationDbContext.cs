using Microsoft.EntityFrameworkCore;
using TestDataApi.Models;

namespace TestDataApi.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Report> Reports { get; set; } = null!;
    public DbSet<Test> Tests { get; set; } = null!;

}