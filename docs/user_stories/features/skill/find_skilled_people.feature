Feature: Find People with Specific Skill
  As a user
  I want to see all people who possess a skill
  So that I can identify potential resources for projects

  Background:
    Given the user is logged into the system
    And the user is on the skill screen for a specific skill

  Scenario: View list of people with the skill
    When the skill screen loads
    Then a list of all people with this skill should be displayed
    And each person should show their proficiency level for this skill

  Scenario: Filter people by proficiency level
    When the user selects a minimum proficiency level from the filter
    Then only people with that proficiency level or higher should be displayed
    And the number of filtered results should be shown

  Scenario Outline: Sort people by proficiency level
    When the user selects to sort by <sort_order>
    Then people should be sorted with proficiency levels in <sort_order> order

    Examples:
      | sort_order    |
      | highest first |
      | lowest first  |

  Scenario: Navigate to person details
    When the user clicks on a person in the list
    Then the system should navigate to the detail screen for that person
