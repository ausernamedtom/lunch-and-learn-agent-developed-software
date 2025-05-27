# ADR-002: Implementing Skill Proficiency Levels

## Status
Proposed (2025-05-27)

## Context
We need to determine how to represent a person's proficiency in a particular skill. Simply indicating that a person has a skill is not sufficient for finding the right people for specific tasks or projects. We need a way to differentiate between beginners, intermediate users, and experts.

## Decision
We will implement a 5-level proficiency scale for all skills:

1. **Novice**: Basic theoretical knowledge, limited practical experience
2. **Beginner**: Can apply the skill with guidance and supervision
3. **Intermediate**: Can work independently on most tasks related to this skill
4. **Advanced**: Deep knowledge, can solve complex problems, can teach others
5. **Expert**: Authoritative knowledge, industry recognition, can innovate in this area

Each skill attached to a person will have a proficiency level assigned. This will be visually represented in the UI with appropriate indicators.

## Consequences
- **Positive**:
  - More granular matching of people to projects based on required skill levels
  - Clear skill development path for team members
  - More meaningful skill searches (e.g., "find all advanced TypeScript developers")
  - Better visualization of team capabilities
  
- **Negative**:
  - Subjectivity in assigning proficiency levels
  - Need for moderation or approval workflow for self-reported skill levels
  - More complex data model and UI components
  - Potential for disagreements about skill assessments
  
## Alternatives Considered
- **Three-level system** (Beginner, Intermediate, Expert): Too simplistic, doesn't provide enough granularity
- **Ten-level system**: Too complex, difficult to meaningfully differentiate between adjacent levels
- **Years of experience**: Not a reliable indicator of skill proficiency
- **No proficiency levels**: Insufficient for meaningful skill matching

## Implementation Notes
The proficiency levels will be stored in the database as integers (1-5), but displayed in the UI with descriptive labels and visual indicators (such as stars or progress bars).
