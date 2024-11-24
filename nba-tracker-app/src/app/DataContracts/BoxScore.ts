import { Team } from './Team';
import { PlayerBoxScore } from './PlayerBoxScore';

export interface BoxScore {
  gameId: string;
  gameTime: Date;
  homeTeam: Team;
  awayTeam: Team;
  homeTeamPlayers: PlayerBoxScore[];
  awayTeamPlayers: PlayerBoxScore[];
}
