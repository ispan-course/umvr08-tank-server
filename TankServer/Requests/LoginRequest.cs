namespace TankServer.Requests;

public class LoginRequest
{
  public string? user { get; set; }
  public string? pass { get; set; }
  public string? nickname { get; set; }
  public int age { get; set; }
  public string? address { get; set; }
}
