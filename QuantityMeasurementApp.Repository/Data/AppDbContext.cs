using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Model.Entity;

namespace QuantityMeasurementApp.Repository.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<QuantityMeasurementEntity> Measurements { get; set; }
  }
}