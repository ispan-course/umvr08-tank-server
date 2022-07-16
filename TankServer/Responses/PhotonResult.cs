namespace TankServer.Responses;

public class PhotonResult
{
  public int ResultCode { get; set; }

  public string? Message { get; set; }

  public long UserId { get; set; }
}
