describe('Scores Page', () => {
  beforeEach(() => {
    cy.fixture('todays-scores-fixture').then((scores) => {
      cy.intercept('GET', '/api/TodaysScores', {
        statusCode: 200,
        body: scores,
      }).as('getScores');
    });
  });

  it('Scores Page Accessibility', () => {
    cy.visit('/scores')
    cy.injectAxe()
    cy.checkA11y()
  })
})
