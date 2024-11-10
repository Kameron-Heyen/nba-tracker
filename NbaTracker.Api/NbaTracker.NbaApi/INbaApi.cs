using NbaTracker.Common.DataContracts;
using NbaTracker.NbaApi.Requests;
using NbaTracker.NbaApi.Responses;

namespace NbaTracker.NbaApi;

public interface INbaApi
{
    public Task<ICollection<Game>> GetTodaysGames();
    public Task<BoxScore> GetBoxScore(string gameId);
}