Feature: View People List
  As a user
  I want to see a list of all people in the system
  So that I can quickly find the right person

  Background:
    Given the user is logged into the system
    And the user is on the overview screen

  Scenario: View all people in the system
    When the overview screen loads
    Then a list of all people should be displayed
    And each person should be represented by a card with their name and avatar

  Scenario: View top skills for each person
    When the overview screen loads
    Then each person's card should display their top skills
    And each skill should show its proficiency level

  Scenario: Navigate to person details
    When the user clicks on a person's card
    Then the system should navigate to the detail screen for that person
    And the detail screen should display that person's full profile
