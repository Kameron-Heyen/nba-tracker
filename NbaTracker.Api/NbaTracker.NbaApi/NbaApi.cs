using System.Data;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using NbaTracker.Common.DataContracts;
using NbaTracker.Common.Enums;

namespace NbaTracker.NbaApi;

public class NbaApi: INbaApi
{
    public async Task<BoxScore> GetBoxScore(string gameId)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"https://cdn.nba.com/static/json/liveData/boxscore/boxscore_{gameId}.json");

        if (!response.IsSuccessStatusCode) throw new HttpRequestException($"Failed to get box score for game {gameId}");
        var content = await response.Content.ReadFromJsonAsync<JsonObject>();
        var gameNode = content?["game"];
        if (gameNode is not null)
        {
            var boxScore = new BoxScore()
            {
                GameId = gameNode["gameId"]?.ToString()?? throw new DataException("Game ID not found"),
                Arena = ParseArena(gameNode["arena"]!),
                GameTime = DateTime.Parse(gameNode["gameTimeUTC"]!.ToString(), CultureInfo.InvariantCulture),
                HomeTeam = ParseTeam(gameNode["homeTeam"]!),
                AwayTeam  = ParseTeam(gameNode["awayTeam"]!),
            };
                
            var homeTeamPlayers = new List<PlayerBoxScore>();
            var awayTeamPlayers = new List<PlayerBoxScore>();

            if (gameNode["homeTeam"]?["players"] is JsonArray {Count: > 0} homePlayerNodes)
            {
                foreach (var playerNode in homePlayerNodes)
                {
                    if (playerNode is not null)
                    {
                        homeTeamPlayers.Add(ParsePlayerBoxScore(playerNode));
                    }
                }
            }
                
            if (gameNode["awayTeam"]?["players"] is JsonArray {Count: > 0} awayPlayerNodes)
            {
                foreach (var playerNode in awayPlayerNodes)
                {
                    if (playerNode is not null)
                    {
                        awayTeamPlayers.Add(ParsePlayerBoxScore(playerNode));
                    }
                }
            }
                
            boxScore.HomeTeamPlayers = homeTeamPlayers.OrderBy(x => x.SortOrder).ToList();
            boxScore.AwayTeamPlayers = awayTeamPlayers.OrderBy(x => x.SortOrder).ToList();
            return boxScore;
        }
        throw new DataException("Game not found");
    }
    
    public async Task<ICollection<Game>> GetTodaysGames()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://cdn.nba.com/static/json/liveData/scoreboard/todaysScoreboard_00.json");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<JsonObject>();
            
            var games = new List<Game>();

            if (content?["scoreboard"]?["games"] is JsonArray {Count: > 0} gamesNode)
            {
                foreach (var gameNode in gamesNode)
                {
                    var game = new Game
                    {
                        GameId = gameNode?["gameId"]?.ToString() ?? throw new DataException("Game ID not found"),
                        GameStatus = Enum.Parse<GameStatus>(gameNode["gameStatus"]?.ToString() ?? "0"),
                        GameStatusText = gameNode["gameStatusText"]?.ToString() ?? "Game Status Unknown",
                        Period = int.Parse(gameNode["period"]?.ToString() ?? "0"),
                        GameTime = DateTime.Parse(gameNode["gameTimeUTC"]!.ToString(), CultureInfo.InvariantCulture),
                        HomeTeam = ParseTeam(gameNode["homeTeam"]!),
                        AwayTeam = ParseTeam(gameNode["awayTeam"]!),
                        HomeGameLeader = ParseGameLeader(gameNode["gameLeaders"]?["homeLeaders"]!),
                        AwayGameLeader = ParseGameLeader(gameNode["gameLeaders"]?["awayLeaders"]!)
                    };

                    games.Add(game);
                }
            }
            return games;
        }
        else
        {
            throw new HttpRequestException("Failed to get today's games");
        }
    }
    
    # region Parsers

    private static PlayerGameStats ParsePlayerGameStats(JsonNode playerStatsNode)
    {
        return new PlayerGameStats()
        {
            Points = int.Parse(playerStatsNode["points"]?.ToString() ?? "0"),
            Assists = int.Parse(playerStatsNode["assists"]?.ToString() ?? "0"),
            OffensiveRebounds = int.Parse(playerStatsNode["reboundsOffensive"]?.ToString() ?? "0"),
            DefensiveRebounds = int.Parse(playerStatsNode["reboundsDefensive"]?.ToString() ?? "0"),
            Blocks = int.Parse(playerStatsNode["blocks"]?.ToString() ?? "0"),
            Steals = int.Parse(playerStatsNode["steals"]?.ToString() ?? "0"),
            Turnovers = int.Parse(playerStatsNode["turnovers"]?.ToString() ?? "0"),
            FieldGoalAttempts = int.Parse(playerStatsNode["fieldGoalsAttempted"]?.ToString() ?? "0"),
            FieldGoalsMade = int.Parse(playerStatsNode["fieldGoalsMade"]?.ToString() ?? "0"),
            ThreePointersAttempted = int.Parse(playerStatsNode["threePointersAttempted"]?.ToString() ?? "0"),
            ThreePointersMade = int.Parse(playerStatsNode["threePointersMade"]?.ToString() ?? "0"),
            FreeThrowsAttempted = int.Parse(playerStatsNode["freeThrowsAttempted"]?.ToString() ?? "0"),
            FreeThrowsMade = int.Parse(playerStatsNode["freeThrowsMade"]?.ToString() ?? "0"),
            PlusMinus = (int)float.Parse(playerStatsNode["plusMinusPoints"]?.ToString() ?? "0"),
            PersonalFouls = int.Parse(playerStatsNode["foulsPersonal"]?.ToString() ?? "0"),
            TechnicalFouls = int.Parse(playerStatsNode["foulsTechnical"]?.ToString() ?? "0"),
        };
    }
    private static PlayerBoxScore ParsePlayerBoxScore(JsonNode playerNode)
    {
        return new PlayerBoxScore()
        {
            PersonId = int.Parse(playerNode?["personId"]?.ToString() ?? throw new DataException("Person ID not found")),
            FirstName = playerNode["firstName"]?.ToString() ?? "Unknown",
            LastName = playerNode["familyName"]?.ToString() ?? "Unknown",
            JerseyNumber = playerNode["jerseyNum"]?.ToString() ?? "0",
            Starter = playerNode["starter"]?.ToString() == "1",
            Played = playerNode["played"]?.ToString() == "1",
            OnCourt = playerNode["onCourt"]?.ToString() == "1",
            SortOrder = int.Parse(playerNode["order"]?.ToString() ?? "0"),
            GameStats = ParsePlayerGameStats(playerNode["statistics"]!),
        };
    }
    
    private static Team ParseTeam(JsonNode teamNode)
    {
        int? wins = null;
        int? losses = null;
        if (teamNode["wins"] is not null)
        {
            wins = int.Parse(teamNode["wins"]!.ToString());
        }
        
        if (teamNode["losses"] is not null)
        {
            losses = int.Parse(teamNode["losses"]!.ToString());
        }
        
        return new Team()
        {
            TeamId = int.Parse((teamNode["teamId"]?.ToString() ?? "0")),
            TeamName = teamNode["teamName"]?.ToString() ?? "Unknown Team",
            TeamCity = teamNode["teamCity"]?.ToString() ?? "Unknown City",
            TeamTricode = teamNode["teamTricode"]?.ToString() ?? "???",
            Score = int.Parse(teamNode["score"]?.ToString() ?? "0"),
            Wins = wins,
            Losses = losses,
            TimeoutsRemaining = int.Parse(teamNode["timeoutsRemaining"]?.ToString() ?? "0"),
        };
    }
    
    private static Arena ParseArena(JsonNode arenaNode)
    {
        return new Arena()
        {
            Name = arenaNode["arenaName"]?.ToString() ?? "Unknown Arena",
            City = arenaNode["arenaCity"]?.ToString() ?? "Unknown City",
            State = arenaNode["arenaState"]?.ToString() ?? "Unknown State",
            Country = arenaNode["arenaCountry"]?.ToString() ?? "Unknown Country",
        };
    }
    
    private static GameLeader ParseGameLeader(JsonNode gameLeaderNode)
    {
        return new GameLeader()
        {
            GameLeaderId = int.Parse(gameLeaderNode?["personId"]?.ToString()??"0"),
            Name = gameLeaderNode?["name"]?.ToString()??"N/A",
            JerseyNumber = gameLeaderNode?["jerseyNum"]?.ToString()??"N/A",
            Position = gameLeaderNode?["position"]?.ToString()??"N/A",
            Points = int.Parse(gameLeaderNode?["points"]?.ToString()??"0"),
            Assists = int.Parse(gameLeaderNode?["assists"]?.ToString()??"0"),
            Rebounds = int.Parse(gameLeaderNode?["rebounds"]?.ToString()??"0"),
        }; 
    }
    # endregion
}