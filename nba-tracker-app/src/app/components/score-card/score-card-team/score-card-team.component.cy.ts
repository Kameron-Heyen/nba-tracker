import { ScoreCardTeamComponent } from './score-card-team.component';
import { Team } from '../../../DataContracts/Team';

describe('ScoreCardTeamComponent', () => {
  let mockTeam: Team;
  beforeEach(function () {
    mockTeam = {
      teamId: 1,
      teamName: 'Timberwolves',
      teamCity: 'Minnesota',
      teamTricode: 'MIN',
      wins: 1,
      losses: 1,
      score: 100,
      timeoutsRemaining: 1,
    };
  });

  it('should mount', () => {
    cy.mount(ScoreCardTeamComponent, {
      componentProperties: { team: mockTeam },
    });
  });

  it('should render team name', () => {
    cy.mount(ScoreCardTeamComponent, {
      componentProperties: { team: mockTeam },
    });
    cy.get('[data-cy="score-card-team-name"]').should(
      'contain.text',
      'Timberwolves',
    );
    cy.get('[data-cy="score-card-team-city"]').should('contain.text', 'Minnesota');
    cy.get('[data-cy="score-card-team-logo"]').should('exist');
  });
});
