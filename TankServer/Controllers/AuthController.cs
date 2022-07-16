using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TankServer.Models;
using TankServer.Responses;

namespace TankServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly TankContext _context;

  public AuthController(TankContext context)
  {
    _context = context;
  }

  // Get: api/auth/login
  [HttpGet("login")]
  public async Task<PhotonResult> Login(string user, string pass)
  {
    // 判斷參數是否有值
    if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
    {
      return new PhotonResult
      {
        ResultCode = 3,
        Message = "Invalid parameters."
      };
    }

    var player = await _context.Players
      .Where(p => p.Name == user)
      .FirstOrDefaultAsync();

    // 產生 password 的 hash 值
    var passwordHash = GetSha256Hash(pass);

    if (player != null)
    {
      // 判斷 hash 後的密碼是一致
      if (player.Password != passwordHash)
      {
        return new PhotonResult
        {
          ResultCode = 2,
          Message = "Credentials incorrect."
        };
      }

      return new PhotonResult
      {
        ResultCode = 1,
        UserId = player.Id
      };
    }

    // 如果玩家不存在，則幫他註冊
    player = new Player
    {
      Name = user,
      Password = passwordHash
    };
    await _context.Players.AddAsync(player);

    // 儲存到資料庫
    await _context.SaveChangesAsync();

    return new PhotonResult
    {
      ResultCode = 1,
      UserId = player.Id
    };
  }

  [NonAction]
  private static string GetSha256Hash(string? input)
  {
    if (input == null)
      throw new ArgumentNullException(nameof(input));

    using var hashAlgorithm = SHA512.Create();
    var byteValue = Encoding.UTF8.GetBytes(input);
    var byteHash = hashAlgorithm.ComputeHash(byteValue);
    return Convert.ToBase64String(byteHash);
  }
}
