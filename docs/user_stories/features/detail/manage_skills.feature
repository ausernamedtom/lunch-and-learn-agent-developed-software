Feature: Manage Person's Skills
  As a user with appropriate permissions
  I want to be able to navigate to related skills or update information
  So that I can maintain accurate profile data

  Background:
    Given the user is logged into the system
    And the user is on the detail screen for a specific person
    And the user has permission to edit profiles

  Scenario: Navigate to skill details
    When the user clicks on a skill in the person's profile
    Then the system should navigate to the skill screen for that skill

  Scenario: Edit person details
    When the user clicks the "Edit Profile" button
    Then the person's information should become editable
    And the user should be able to save or cancel the changes

  Scenario: Add new skill to person
    When the user clicks "Add Skill"
    Then a skill selection interface should appear
    And the user should be able to select a skill and proficiency level
    And the user should be able to save the new skill to the person's profile

  Scenario: Update skill proficiency
    When the user selects a skill from the person's profile
    And clicks "Update Proficiency"
    Then the user should be able to change the proficiency level
    And save the updated proficiency

  Scenario: Remove skill from person
    When the user selects a skill from the person's profile
    And clicks "Remove Skill"
    Then a confirmation prompt should appear
    And if confirmed, the skill should be removed from the person's profile
