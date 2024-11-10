using NbaTracker.Common.DataContracts;
using NbaTracker.NbaApi;

namespace NbaTracker.GameManager;

public class GameManager(INbaApi nbaApi): IGameManager
{
    public async Task<ICollection<Game>> GetTodaysGames()
    {
        var games = await nbaApi.GetTodaysGames();
        return games;
    }
    
    public async Task<BoxScore> GetBoxScore(string gameId)
    {
        var boxScore = await nbaApi.GetBoxScore(gameId);
        return boxScore;
    }
}