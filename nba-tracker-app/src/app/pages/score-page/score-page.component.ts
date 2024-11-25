import { Component, OnInit } from '@angular/core';
import { ScoreService } from '../../services/score.service';
import { ScoreCardComponent } from '../../components/score-card/score-card.component';
import { NgForOf, NgIf } from '@angular/common';
import { Game } from '../../DataContracts/Game';
import { GameStatusEnum } from '../../DataContracts/enums/GameStatusEnum';

@Component({
  selector: 'app-score-page',
  standalone: true,
  imports: [ScoreCardComponent, NgForOf, NgIf],
  templateUrl: './score-page.component.html',
  styleUrl: './score-page.component.scss',
})
export class ScorePageComponent implements OnInit {
  games!: Game[];
  loading = true;

  constructor(private readonly scoreService: ScoreService) {}

  ngOnInit() {
    this.loadGames();
  }

  private loadGames() {
    this.loading = true;
    this.scoreService.getTodaysGames().subscribe((data) => {
      this.games = data;
      this.loading = false;
    });
  }

  get InProgressGames() {
    return this.games.filter((g) => g.gameStatus === GameStatusEnum.InProgress);
  }

  get CompletedGames() {
    return this.games.filter((g) => g.gameStatus === GameStatusEnum.Final);
  }

  get UpcomingGames() {
    return this.games.filter((g) => g.gameStatus === GameStatusEnum.NotStarted);
  }
}
