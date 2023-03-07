using System.Text.Json;
using Microsoft.EntityFrameworkCore;



public class DataContext : DbContext
{

  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {


  }
  public DbSet<Flight> Flights { get; set; } = default!;
  public DbSet<FlightRoute> FlightRoutes { get; set; } = default!;
  public DbSet<Price> Prices { get; set; } = default!;

  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {


        modelBuilder.Entity<Price>()
        .Property(p => p.Adult)
        .HasColumnType("decimal(18,2)");

    modelBuilder.Entity<Price>()
        .Property(p => p.Child)
        .HasColumnType("decimal(18,2)");



  }


} 
