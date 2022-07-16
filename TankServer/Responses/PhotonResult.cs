namespace TankServer.Responses;

public class PhotonResult
{
  public int ResultCode { get; set; }

  public string? Message { get; set; }

  public long UserId { get; set; }
  
  public string? Nickname { get; set; }
  
  public Dictionary<string, object>? AuthCookie { get; set; }

  public Dictionary<string, object>? Data { get; set; }
}
