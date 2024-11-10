namespace NbaTracker.Common.DataContracts;

public class BoxScore
{
    public string GameId { get; set; }
    public DateTime GameTime { get; set; }
    public Arena Arena { get; set; }
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public ICollection<PlayerBoxScore> HomeTeamPlayers { get; set; }
    public ICollection<PlayerBoxScore> AwayTeamPlayers { get; set; }
}