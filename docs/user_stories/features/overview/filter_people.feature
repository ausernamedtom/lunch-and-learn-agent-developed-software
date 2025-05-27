Feature: Filter and Search People
  As a user
  I want to filter or search for people
  So that I can quickly find specific individuals

  Background:
    Given the user is logged into the system
    And the user is on the overview screen
    And the system displays a list of people

  Scenario: Filter people by skill
    When the user selects a skill from the filter dropdown
    Then only people with that skill should be displayed
    And the filter selection should be visually indicated

  Scenario: Search for people by name
    When the user enters a name in the search field
    Then only people whose names contain the search term should be displayed
    And the search term should be highlighted in the results

  Scenario: Clear filters and search
    Given the user has applied filters or search terms
    When the user clicks the "Clear" button
    Then all filters and search terms should be removed
    And all people should be displayed again

  Scenario Outline: Filter people by proficiency level
    When the user selects proficiency level <level> from the filter
    Then only people with skills at that proficiency level or higher should be displayed

    Examples:
      | level     |
      | Novice    |
      | Beginner  |
      | Intermediate |
      | Advanced  |
      | Expert    |
