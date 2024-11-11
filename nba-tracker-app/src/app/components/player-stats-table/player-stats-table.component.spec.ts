import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerStatsTableComponent } from './player-stats-table.component';
import {BoxScore} from "../../DataContracts/BoxScore";

describe('PlayerStatsTableComponent', () => {
  let component: PlayerStatsTableComponent;
  let fixture: ComponentFixture<PlayerStatsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlayerStatsTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlayerStatsTableComponent);
    component = fixture.componentInstance;

    const boxScore: BoxScore = {
      gameId: "1",
      gameTime: new Date(),
      homeTeam: {
        teamId: 1,
        teamName: "Home",
        wins: 0,
        losses: 0,
        teamTricode: "HOM",
        teamCity: "Home",
        score: 0,
        timeoutsRemaining: 0
      },
      awayTeam: {
        teamId: 2,
        teamName: "Away",
        wins: 0,
        losses: 0,
        teamTricode: "AWY",
        teamCity: "Away",
        score: 0,
        timeoutsRemaining: 0
      },
      homeTeamPlayers: [],
      awayTeamPlayers: []
    };
    component.boxScore = boxScore;
    fixture.detectChanges();
  });

  it('should create', () => {

    expect(component).toBeTruthy();
  });
});
