import { ScoreCardComponent } from './score-card.component';
import { provideHttpClient } from '@angular/common/http';
import { Game } from '../../DataContracts/Game';

describe('ScoreCardComponent', () => {
  let mockGame: Game;
  beforeEach(function () {
    cy.fixture('box-score-fixture').then((scores) => {
      cy.intercept('GET', '/api/BoxScore/*', {
        statusCode: 200,
        body: scores,
      }).as('getBoxScore');
    });

    cy.fixture('todays-scores-fixture').then((scores) => {
      mockGame = scores[0];
    });
  });

  it('should mount', () => {
    cy.mount(ScoreCardComponent, {
      providers: [provideHttpClient()],
      componentProperties: { game: mockGame },
    });
  });

  it('should open box score', () => {
    cy.mount(ScoreCardComponent, {
      providers: [provideHttpClient()],
      componentProperties: { game: mockGame },
    });

    cy.get('[data-cy="box-score-dropdown"]').should('not.exist');
    cy.get('[data-cy="local-score-card"]').click();
    cy.wait('@getBoxScore');
    cy.get('[data-cy="box-score-dropdown"]').should('exist');
  });
});
