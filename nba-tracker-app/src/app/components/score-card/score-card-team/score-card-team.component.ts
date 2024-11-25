import { Component, Input, OnInit } from '@angular/core';
import { Team } from '../../../DataContracts/Team';
import { BrandingService } from '../../../services/branding.service';
import { TeamBranding } from '../../../DataContracts/TeamBranding';
import { NgIf, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-score-card-team',
  standalone: true,
  imports: [NgOptimizedImage, NgIf],
  templateUrl: './score-card-team.component.html',
  styleUrl: './score-card-team.component.scss',
})
export class ScoreCardTeamComponent implements OnInit {
  @Input({ required: true }) team!: Team;

  branding?: TeamBranding;

  constructor(private readonly brandingService: BrandingService) {}

  ngOnInit() {
    this.branding = this.brandingService.getTeamBranding(this.team.teamTricode);
  }
}
