using NbaTracker.Common.Enums;

namespace NbaTracker.Common.DataContracts;

public class Game
{
  public required string GameId { get; set; }
  public GameStatus GameStatus { get; set; }
  public required string GameStatusText { get; set; }
  public int Period { get; set; }
  public required DateTime GameTime { get; set; }
  
  public required Team HomeTeam { get; set; }
  public required Team AwayTeam { get; set; }
  
  public required GameLeader HomeGameLeader { get; set; }
  public required GameLeader AwayGameLeader { get; set; }
}