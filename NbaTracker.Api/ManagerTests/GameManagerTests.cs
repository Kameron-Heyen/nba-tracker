using System.Data;
using Moq;
using NbaTracker.Common.DataContracts;
using NbaTracker.Common.Enums;
using NbaTracker.GameManager;
using NbaTracker.NbaApi;

namespace UtilityTests;

public class GameManagerTests
{
    private IGameManager sut; 
    private Mock<INbaApi> NbaApiMock = new(MockBehavior.Strict);
    
    [SetUp]
    public void Setup()
    {
        sut = new GameManager(NbaApiMock.Object);
    }
    
    [Test]
    public async Task GameManager_GetTodaysGames_empty()
    {
        // Arrange
        var todaysGames = new List<Game>();

        NbaApiMock.Setup(x => x.GetTodaysGames()).ReturnsAsync(todaysGames);
        
        // Act
        var games = await sut.GetTodaysGames();

        // Assert
        Assert.IsNotNull(games);
        Assert.IsEmpty(games);
    }
    
    [Test]
    public async Task GameManager_GetTodaysGames_SingleGame()
    {
        // Arrange
        var todaysGames = new List<Game>() {
            new Game
            {
                GameId = "1",
                GameTime = DateTime.Now,
                GameStatus = GameStatus.InProgress,
                GameStatusText = "test",
                HomeGameLeader = new GameLeader()
                {
                    Assists = 1,
                    Points = 1,
                    Rebounds = 1,
                    Name = "test",
                    JerseyNumber = "1",
                    Position = "C",
                    GameLeaderId = 1
                },
                AwayGameLeader = new GameLeader()
                {
                    Assists = 1,
                    Points = 1,
                    Rebounds = 1,
                    Name = "test",
                    JerseyNumber = "1",
                    Position = "C",
                    GameLeaderId = 1
                },
                HomeTeam = new Team()
                {
                    Score = 100,
                    TeamId = 1,
                    TeamCity = "Lincoln",
                    TeamName = "Huskers",
                    TeamTricode = "LNK",
                    TimeoutsRemaining = 0
                },
                AwayTeam = new Team()
                {
                    Score = 99,
                    TeamId = 2,
                    TeamCity = "Kansas City",
                    TeamName = "Chiefs",
                    TeamTricode = "KCC",
                    TimeoutsRemaining = 0
                }
            }
        };

        NbaApiMock.Setup(x => x.GetTodaysGames()).ReturnsAsync(todaysGames);
        
        // Act
        var games = await sut.GetTodaysGames();

        // Assert
        Assert.IsNotNull(games);
        Assert.IsNotEmpty(games);
    }
    
    [Test]
    public async Task GameManager_GetBoxScore_NotFound()
    {
        NbaApiMock.Setup(x => x.GetBoxScore(It.IsAny<string>())).Throws(new DataException("Game not found"));
        var ex = Assert.ThrowsAsync<DataException>(async () => await sut.GetBoxScore("test"));
        Assert.That(ex.Message, Is.EqualTo("Game not found"));
    }
}
