import { Component, Input, OnInit } from '@angular/core';
import { Game } from '../../DataContracts/Game';
import { NgIf } from '@angular/common';
import { PlayerStatsTableComponent } from '../player-stats-table/player-stats-table.component';
import { BoxScore } from '../../DataContracts/BoxScore';
import { ScoreService } from '../../services/score.service';

@Component({
  selector: 'app-score-card',
  standalone: true,
  imports: [NgIf, PlayerStatsTableComponent],
  templateUrl: './score-card.component.html',
  styleUrl: './score-card.component.scss',
})
export class ScoreCardComponent implements OnInit {
  @Input({ required: true }) game!: Game;

  expanded = false;
  boxScoreLoading = false;
  boxScore: BoxScore | null = null;

  constructor(private readonly scoreService: ScoreService) {}

  ngOnInit() {}

  toggleExpand() {
    if (!this.expanded) {
      if (!this.boxScore) {
        this.boxScoreLoading = true;
        this.scoreService
          .getBoxScore(this.game.gameId)
          .subscribe((boxScore) => {
            this.boxScore = boxScore;
            console.log(this.boxScore);
            this.boxScoreLoading = false;
          });
      }
    }
    this.expanded = !this.expanded;
  }
}
