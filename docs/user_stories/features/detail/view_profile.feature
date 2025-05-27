Feature: View Person Profile
  As a user
  I want to see all details about a person
  So that I can understand their full profile

  Background:
    Given the user is logged into the system
    And the user is on the detail screen for a specific person

  Scenario: View basic person information
    When the detail screen loads
    Then the person's name should be displayed
    And the person's avatar should be displayed
    And the person's basic information should be displayed

  Scenario: View all skills for a person
    When the detail screen loads
    Then a list of all the person's skills should be displayed
    And each skill should show its proficiency level
    And the skills should be categorized by type

  Scenario: View skill proficiency levels
    When looking at the person's skills list
    Then each skill should display a proficiency level from 1 to 5
    And the proficiency level should be visually indicated
    And the meaning of each proficiency level should be available
