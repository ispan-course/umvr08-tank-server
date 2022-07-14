using Microsoft.EntityFrameworkCore;

namespace TankServer.Models;

public class TankContext : DbContext
{
  public TankContext(DbContextOptions<TankContext> options) : base(options)
  {
  }

  public DbSet<Player> Players { get; set; } = null!;
}
