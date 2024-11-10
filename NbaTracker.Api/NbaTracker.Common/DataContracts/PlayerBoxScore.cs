namespace NbaTracker.Common.DataContracts;

public class PlayerBoxScore
{
    public int PersonId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string JerseyNumber { get; set; }
    public bool Starter { get; set; }
    public bool Played { get; set; }
    public bool OnCourt { get; set; }
    public int SortOrder { get; set; }
    public PlayerGameStats GameStats { get; set; } = new PlayerGameStats();
}