namespace NbaTracker.Common.DataContracts;

public class GameLeader
{
    public required int GameLeaderId { get; set; }
    public required string Name { get; set; } 
    public required string JerseyNumber { get; set; }
    public required string Position { get; set; }
    public required int Points { get; set; }
    public required int Rebounds { get; set; }
    public required int Assists { get; set; }
}