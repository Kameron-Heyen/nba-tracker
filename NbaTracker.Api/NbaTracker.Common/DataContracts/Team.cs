namespace NbaTracker.Common.DataContracts;

public class Team
{
    public required int TeamId { get; set; }
    public required string TeamName { get; set; }
    public required string TeamCity { get; set; }
    public required string TeamTricode { get; set; }
    public int? Wins { get; set; }
    public int? Losses { get; set; }
    public required int Score { get; set; }
    public required int TimeoutsRemaining { get; set; }
}