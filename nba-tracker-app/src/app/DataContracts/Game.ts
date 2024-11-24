import { GameStatusEnum } from './enums/GameStatusEnum';
import { Team } from './Team';
import { GameLeader } from './GameLeader';

export interface Game {
  gameId: string;
  gameStatus: GameStatusEnum;
  gameStatusText: string;
  period: number;
  gameTime: Date;

  homeTeam: Team;
  awayTeam: Team;

  homeGameLeader: GameLeader;
  awayGameLeader: GameLeader;
}
