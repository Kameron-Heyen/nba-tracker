import { Component, Input } from '@angular/core';
import { BoxScore } from '../../DataContracts/BoxScore';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-player-stats-table',
  standalone: true,
  imports: [NgForOf],
  templateUrl: './player-stats-table.component.html',
  styleUrl: './player-stats-table.component.scss',
})
export class PlayerStatsTableComponent {
  @Input({ required: true }) boxScore!: BoxScore;
}
