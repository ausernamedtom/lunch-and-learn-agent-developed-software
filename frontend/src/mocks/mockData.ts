import { Person, ProficiencyLevel, Skill } from '../types';

// Mock Skills Data
export const mockSkills: Skill[] = [
  {
    id: 'skill-1',
    name: 'React',
    description: 'A JavaScript library for building user interfaces',
    category: 'Frontend'
  },
  {
    id: 'skill-2',
    name: 'TypeScript',
    description: 'A strongly typed programming language that builds on JavaScript',
    category: 'Programming Language'
  },
  {
    id: 'skill-3',
    name: 'C#',
    description: 'A modern object-oriented programming language',
    category: 'Backend'
  },
  {
    id: 'skill-4',
    name: 'Azure SQL',
    description: 'Microsoft cloud database solution',
    category: 'Database'
  },
  {
    id: 'skill-5',
    name: 'Tailwind CSS',
    description: 'A utility-first CSS framework',
    category: 'Frontend'
  },
  {
    id: 'skill-6',
    name: 'Entity Framework',
    description: 'An ORM framework for .NET applications',
    category: 'Backend'
  },
  {
    id: 'skill-7',
    name: 'REST API Design',
    description: 'Designing RESTful APIs',
    category: 'Backend'
  },
  {
    id: 'skill-8',
    name: 'HATEOAS',
    description: 'Hypermedia as the Engine of Application State',
    category: 'API Design'
  }
];

// Mock People Data
export const mockPeople: Person[] = [
  {
    id: 'person-1',
    firstName: 'John',
    lastName: 'Doe',
    jobTitle: 'Senior Frontend Developer',
    department: 'Engineering',
    email: 'john.doe@example.com',
    photoUrl: 'https://randomuser.me/api/portraits/men/1.jpg',
    skills: [
      { skillId: 'skill-1', proficiencyLevel: ProficiencyLevel.Expert, yearsOfExperience: 5, isVerified: true },
      { skillId: 'skill-2', proficiencyLevel: ProficiencyLevel.Advanced, yearsOfExperience: 3, isVerified: true },
      { skillId: 'skill-5', proficiencyLevel: ProficiencyLevel.Intermediate, yearsOfExperience: 2, isVerified: false }
    ]
  },
  {
    id: 'person-2',
    firstName: 'Jane',
    lastName: 'Smith',
    jobTitle: 'Backend Developer',
    department: 'Engineering',
    email: 'jane.smith@example.com',
    photoUrl: 'https://randomuser.me/api/portraits/women/2.jpg',
    skills: [
      { skillId: 'skill-3', proficiencyLevel: ProficiencyLevel.Expert, yearsOfExperience: 6, isVerified: true },
      { skillId: 'skill-6', proficiencyLevel: ProficiencyLevel.Advanced, yearsOfExperience: 4, isVerified: true },
      { skillId: 'skill-4', proficiencyLevel: ProficiencyLevel.Intermediate, yearsOfExperience: 2, isVerified: true }
    ]
  },
  {
    id: 'person-3',
    firstName: 'Mike',
    lastName: 'Johnson',
    jobTitle: 'Full Stack Developer',
    department: 'Engineering',
    email: 'mike.johnson@example.com',
    photoUrl: 'https://randomuser.me/api/portraits/men/3.jpg',
    skills: [
      { skillId: 'skill-1', proficiencyLevel: ProficiencyLevel.Advanced, yearsOfExperience: 4, isVerified: true },
      { skillId: 'skill-3', proficiencyLevel: ProficiencyLevel.Intermediate, yearsOfExperience: 2, isVerified: false },
      { skillId: 'skill-4', proficiencyLevel: ProficiencyLevel.Beginner, yearsOfExperience: 1, isVerified: false }
    ]
  },
  {
    id: 'person-4',
    firstName: 'Emily',
    lastName: 'Davis',
    jobTitle: 'API Designer',
    department: 'Architecture',
    email: 'emily.davis@example.com',
    photoUrl: 'https://randomuser.me/api/portraits/women/4.jpg',
    skills: [
      { skillId: 'skill-7', proficiencyLevel: ProficiencyLevel.Expert, yearsOfExperience: 7, isVerified: true },
      { skillId: 'skill-8', proficiencyLevel: ProficiencyLevel.Expert, yearsOfExperience: 5, isVerified: true },
      { skillId: 'skill-3', proficiencyLevel: ProficiencyLevel.Advanced, yearsOfExperience: 4, isVerified: true }
    ]
  },
  {
    id: 'person-5',
    firstName: 'Robert',
    lastName: 'Brown',
    jobTitle: 'Database Administrator',
    department: 'IT Operations',
    email: 'robert.brown@example.com',
    photoUrl: 'https://randomuser.me/api/portraits/men/5.jpg',
    skills: [
      { skillId: 'skill-4', proficiencyLevel: ProficiencyLevel.Expert, yearsOfExperience: 8, isVerified: true },
      { skillId: 'skill-6', proficiencyLevel: ProficiencyLevel.Intermediate, yearsOfExperience: 3, isVerified: false },
      { skillId: 'skill-3', proficiencyLevel: ProficiencyLevel.Beginner, yearsOfExperience: 1, isVerified: false }
    ]
  }
];

// Helper function to get a skill by ID
export const getSkillById = (id: string): Skill | undefined => {
  return mockSkills.find(skill => skill.id === id);
};

// Helper function to get a person by ID
export const getPersonById = (id: string): Person | undefined => {
  return mockPeople.find(person => person.id === id);
};

// Helper function to get all people with a specific skill
export const getPeopleWithSkill = (skillId: string): Person[] => {
  return mockPeople.filter(person => 
    person.skills.some(skill => skill.skillId === skillId)
  );
};

// Helper function to get a person's top skills (highest proficiency levels)
export const getPersonTopSkills = (personId: string, limit: number = 3): Array<{ skill: Skill, proficiency: ProficiencyLevel }> => {
  const person = getPersonById(personId);
  if (!person) return [];
  
  // Sort skills by proficiency level (highest first)
  const sortedSkills = [...person.skills].sort((a, b) => b.proficiencyLevel - a.proficiencyLevel);
  
  // Take top N skills and map to skill objects
  return sortedSkills.slice(0, limit).map(ps => ({
    skill: getSkillById(ps.skillId) as Skill, // Type assertion since we know these skills exist
    proficiency: ps.proficiencyLevel
  }));
};
