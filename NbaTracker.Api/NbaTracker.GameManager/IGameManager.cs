using NbaTracker.Common.DataContracts;

namespace NbaTracker.GameManager;

public interface IGameManager
{
    public Task<ICollection<Game>> GetTodaysGames();
    public Task<BoxScore> GetBoxScore(string gameId);
}