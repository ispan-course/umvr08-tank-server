namespace TankServer.Models;

public class Player
{
  public long Id { get; set; }
  public string? Name { get; set; }
  public string? Password { get; set; }
  public int Score { get; set; }
}
