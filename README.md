# NBA Tracker

A simple web application to view NBA games and box scores. Planned work includes displaying conference standings, historical data, and user preferences.

## Setup Instructions
1. Clone the repository

### Frontend
1. ```bash 
    cd ./nba-tracker-app
    ```
2. ```bash
    npm install
    ```
3. ```bash
    ng serve
    ```
### Backend
1. Open `NbaTracker.sln` in Visual Studio or Rider
2. Run `NbaTracker.Functions` project

## Features
- [x] Display Today's NBA games
- [x] Display Box Score for a specific game

## Planned Features
- [ ] Historical Database
- [ ] Display Conference Standings
- [ ] Display NBA games for a specific date
- [ ] Display NBA games for a specific team
- [ ] Allow user to select favorite team(s)
- [ ] Allow user to select favorite player(s)
- [ ] Allow user to import fantasy team roster(s)
- [ ] Sort games by user preference
  - [ ] By favorite team
  - [ ] By favorite player
  - [ ] By fantasy team roster (i.e. sort by games where roster players are playing)

## Data Limitations
Official NBA API only provides data for the current day. 
A historical database will need to be built to enable features involving past games.

## Tech Stack
- Angular 18
- .NET Core
- Azure Functions
