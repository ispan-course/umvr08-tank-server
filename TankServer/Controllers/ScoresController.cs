using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TankServer.Models;
using TankServer.Requests;
using TankServer.Responses;

namespace TankServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScoresController : ControllerBase
{
  private readonly TankContext _context;

  public ScoresController(TankContext context)
  {
    _context = context;
  }

  [HttpPost]
  public async Task<PhotonResult> post([FromBody] ScoreRequest request)
  {
    var userId = int.Parse(request.UserId!);
    var player = await _context.Players
      .Where(p => p.Id == userId)
      .FirstOrDefaultAsync();

    if (player == null)
    {
      return new PhotonResult
      {
        ResultCode = 1,
        Message = "Not Login"
      };
    }

    // 儲存到資料庫
    player.Score = request.score;
    await _context.SaveChangesAsync();

    // Leaderboard
    var players = await _context.Players
      .Take(10)
      .OrderByDescending(p => p.Score)
      .ToListAsync();

    var leaderboard = players
      .ToDictionary<Player, string, object>(p => p.Id.ToString(), p => p.Score);

    return new PhotonResult
    {
      ResultCode = 0,
      Data = leaderboard
    };
  }
}
