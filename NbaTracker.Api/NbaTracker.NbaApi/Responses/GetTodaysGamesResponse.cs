using NbaTracker.Common.DataContracts;

namespace NbaTracker.NbaApi.Responses;

public class GetTodaysGamesResponse
{
    public ICollection<Game> Games { get; set; } = new List<Game>();
}