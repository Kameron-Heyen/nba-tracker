using NbaTracker.Common.DataContracts;

namespace NbaTracker.NbaApi;

public interface INbaApi
{
    public Task<ICollection<Game>> GetTodaysGames();
    public Task<BoxScore> GetBoxScore(string gameId);
}