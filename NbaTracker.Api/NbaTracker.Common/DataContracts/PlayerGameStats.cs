namespace NbaTracker.Common.DataContracts;

public class PlayerGameStats
{
    public int Points { get; set; }
    public int Assists { get; set; }
    public int OffensiveRebounds { get; set; }
    public int DefensiveRebounds { get; set; }
    public int Blocks { get; set; }
    public int Steals { get; set; }
    public int Turnovers { get; set; }
    
    
    public int FieldGoalAttempts { get; set; }
    public int FieldGoalsMade { get; set; }

    public int ThreePointersAttempted { get; set; }
    public int ThreePointersMade { get; set; }

    public int FreeThrowsAttempted { get; set; }
    public int FreeThrowsMade { get; set; }

    
    public int PlusMinus { get; set; }
    
    public int PersonalFouls { get; set; }
    public int TechnicalFouls { get; set; }
    
    // TODO: Add minutes, requires custom parsing - public int Minutes { get; set; }
}