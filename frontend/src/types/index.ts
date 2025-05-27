// Skill proficiency levels as per ADR-002
export enum ProficiencyLevel {
  Novice = 1,      // Basic theoretical knowledge, limited practical experience
  Beginner = 2,    // Can apply the skill with guidance and supervision
  Intermediate = 3, // Can work independently on most tasks related to this skill
  Advanced = 4,    // Deep knowledge, can solve complex problems, can teach others
  Expert = 5       // Authoritative knowledge, industry recognition, can innovate in this area
}

// Proficiency level labels for display purposes
export const ProficiencyLevelLabel: Record<ProficiencyLevel, string> = {
  [ProficiencyLevel.Novice]: 'Novice',
  [ProficiencyLevel.Beginner]: 'Beginner',
  [ProficiencyLevel.Intermediate]: 'Intermediate',
  [ProficiencyLevel.Advanced]: 'Advanced',
  [ProficiencyLevel.Expert]: 'Expert'
};

// Interface for Skill
export interface Skill {
  id: string;
  name: string;
  description: string;
  category: string;
}

// Interface for Person's Skill with proficiency level
export interface PersonSkill {
  skillId: string;
  proficiencyLevel: ProficiencyLevel;
  yearsOfExperience?: number;
  isVerified: boolean;
}

// Interface for Person
export interface Person {
  id: string;
  firstName: string;
  lastName: string;
  jobTitle: string;
  department: string;
  email: string;
  photoUrl?: string;
  skills: PersonSkill[];
}
