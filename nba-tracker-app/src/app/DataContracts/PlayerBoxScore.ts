import {PlayerGameStats} from "./PlayerGameStats";

export interface PlayerBoxScore {
  personId: number;
  firstName: string;
  lastName: string;
  jerseyNumber: string;
  starter: boolean;
  played: boolean;
  onCourt: boolean;
  SortOrder: number;
  gameStats: PlayerGameStats;
}
