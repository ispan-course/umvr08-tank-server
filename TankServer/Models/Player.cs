namespace TankServer.Models;

public class Player
{
  public long Id { get; set; }
  public string? Name { get; set; }
  public string? Password { get; set; }
  public string? Nickname { get; set; }
  public int Age { get; set; }
  public string? Address { get; set; }
  public int Score { get; set; }
}
