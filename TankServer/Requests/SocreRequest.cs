namespace TankServer.Requests;

public class ScoreRequest
{
  public string? AppId { get; set; }
  public string? AppVersion { get; set; }
  public string? Region { get; set; }
  public string? UserId { get; set; }
  public int score { get; set; }
}
