describe('Scores Page', () => {
  beforeEach(() => {
    cy.fixture('todays-scores-fixture').then((scores) => {
      cy.intercept('GET', '/api/TodaysScores', {
        statusCode: 200,
        body: scores,
      }).as('getScores');
    });

    cy.fixture('box-score-fixture').then((scores) => {
      cy.intercept('GET', '/api/BoxScore/*', {
        statusCode: 200,
        body: scores,
      }).as('getBoxScore');
    });
  });

  it('Scores Page Accessibility', () => {
    cy.visit('/scores')
    cy.injectAxe()
    cy.checkA11y()
    cy.get('[data-cy=games-in-progress-col]').first().find('app-score-card').click();
    cy.checkA11y()
  });

  it('Clicking Score Card loads box score', () => {
    cy.visit('/scores')
    const scoreCard= cy.get('[data-cy=games-in-progress-col]').first().find('app-score-card');
    scoreCard.click();
    cy.wait('@getBoxScore');
    cy.get('[data-cy=stats-table]').should('exist');

    scoreCard.click();
    cy.get('[data-cy=stats-table]').should('not.exist');
  });
});
