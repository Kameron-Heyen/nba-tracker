using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NbaTracker.GameManager;

namespace NbaTracker.Api;

public class ScoresFunction(IGameManager gameManager, ILogger<ScoresFunction> logger)
{
    private readonly ILogger<ScoresFunction> _logger = logger;
    

    [Function("TodaysScores")]
    public async Task<IActionResult> GetTodaysScores([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://cdn.nba.com/static/json/liveData/scoreboard/todaysScoreboard_00.json");

        var games = await gameManager.GetTodaysGames();
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(games);
        }
        else
        {
            return new StatusCodeResult((int)response.StatusCode);
        }
    }
    
    [Function("BoxScore")]
    public async Task<IActionResult> GetBoxScore([HttpTrigger(AuthorizationLevel.Function, "get", Route="BoxScore/{gameId}")] HttpRequest req, string gameId)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"https://cdn.nba.com/static/json/liveData/boxscore/boxscore_{gameId}.json");
        
        var boxScore = await gameManager.GetBoxScore(gameId);
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(boxScore);
        }
        else
        {
            return new StatusCodeResult((int)response.StatusCode);
        }
    }

}