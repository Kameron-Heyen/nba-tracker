import { PlayerStatsTableComponent } from './player-stats-table.component';
import { BoxScore } from '../../DataContracts/BoxScore';

describe('PlayerStatsTableComponent', () => {
  let mockBoxScore: BoxScore;
  beforeEach(function () {
    cy.fixture('box-score-fixture').then((boxScore) => {
      mockBoxScore = boxScore;
    });
  });

  it('should mount', () => {
    cy.mount(PlayerStatsTableComponent, {
      componentProperties: {
        boxScore: mockBoxScore,
      },
    });
  });

  it('should navigate between home and away team box scores', () => {
    cy.mount(PlayerStatsTableComponent, {
      componentProperties: {
        boxScore: mockBoxScore,
      },
    });
    cy.get('[data-cy=away-team-stats-nav-button]').click();
    cy.get('[data-cy=away-team-stats-tab]').should('be.visible');
    cy.get('[data-cy=home-team-stats-tab]').should('not.be.visible');
  });
});
