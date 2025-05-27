# ADR-003: Skill Verification Process

## Status
Proposed (2025-05-27)

## Context
As we implement the skill management system with proficiency levels, we need a process to ensure the accuracy of skill claims. Without verification, there's a risk of inflated skill levels or misrepresentation, which would undermine the system's value.

## Decision
We will implement a multi-stage verification process for skills:

1. **Self-declaration**: Users can add skills to their profile with a self-assessed proficiency level
2. **Peer endorsements**: Colleagues can endorse skills, adding credibility
3. **Manager verification**: For critical skills, managers can verify skill claims
4. **Certification upload**: Users can attach certifications as evidence
5. **Skill challenges**: Optional technical assessments for objective verification

The UI will visually differentiate between verified and unverified skills.

## Consequences
- **Positive**:
  - Increased confidence in skill data accuracy
  - Multiple verification methods accommodate different skill types
  - Creates accountability for skill claims
  - Provides motivation for skill development
  
- **Negative**:
  - More complex user experience
  - Potential for interpersonal conflicts during verification
  - Additional database and backend complexity
  - Need for notification system for endorsement requests
  
## Alternatives Considered
- **No verification**: Simplest but prone to inaccuracy
- **Manager-only verification**: Creates bottleneck and dependency
- **External assessment only**: Too resource-intensive for all skills
- **Peer-only system**: May lead to reciprocal endorsements without merit

## Implementation Phases
1. Start with self-declaration and simple peer endorsement
2. Add certification uploads and manager verification
3. Implement skill challenges for technical skills if deemed necessary
