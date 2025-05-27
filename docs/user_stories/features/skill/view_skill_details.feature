Feature: View Skill Details
  As a user
  I want to see detailed information about a specific skill
  So that I can understand its requirements and applications

  Background:
    Given the user is logged into the system
    And the user is on the skill screen for a specific skill

  Scenario: View skill information
    When the skill screen loads
    Then the skill name should be displayed
    And the skill description should be displayed
    And any related categories or tags should be displayed

  Scenario: View skill requirements
    When the skill screen loads
    Then any prerequisites or requirements for the skill should be displayed
    And any related skills should be displayed
    And links to learning resources should be available if applicable

  Scenario: View skill usage metrics
    When the skill screen loads
    Then statistics about how many people have this skill should be displayed
    And distribution of proficiency levels should be visualized
