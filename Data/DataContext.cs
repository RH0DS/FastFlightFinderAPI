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
    // modelBuilder.Entity<User>()
    //   .HasOne(u => u.Store)
    //   .WithMany(s => s.Users)
    //   .HasForeignKey(u => u.StoreId);

    // modelBuilder.Entity<Store>()
    //   .HasMany(s => s.Products)
    //   .WithOne(p => p.Store)
    //   .HasForeignKey(p => p.StoreId);
    // modelBuilder.Entity<UserRole>()
    //   .HasOne(x => x.Role)
    //   .WithMany(y => y.UserRoles)
    //   .HasForeignKey(x => x.RoleId);

    // modelBuilder.Entity<UserRole>()
    //   .HasOne(x => x.User)
    //   .WithMany(y => y.UserRoles)
    //   .HasForeignKey(x => x.UserId);


  }


} 
