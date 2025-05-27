import React, { useState, useEffect } from 'react';
import { getPeopleWithSkill, getSkillById } from '../mocks/mockData';
import { Skill } from '../types';
import PersonCard from '../components/PersonCard';

interface SkillScreenProps {
  skillId: string;
  onPersonClick: (personId: string) => void;
}

/**
 * Skill Screen - Shows information about a specific skill and people who possess it
 */
const SkillScreen: React.FC<SkillScreenProps> = ({ skillId, onPersonClick }) => {
  const [skill, setSkill] = useState<Skill | undefined>(getSkillById(skillId));
  // State for proficiency filter
  const [minProficiencyFilter, setMinProficiencyFilter] = useState(1);
  
  // Update skill when skillId changes
  useEffect(() => {
    setSkill(getSkillById(skillId));
  }, [skillId]);
  
  if (!skill) {
    return <div className="p-6">Skill not found</div>;
  }

  // All people with this skill
  const allPeopleWithSkill = getPeopleWithSkill(skill.id);
  
  // Filter people based on minimum proficiency level
  const peopleWithSkill = allPeopleWithSkill.filter(person => {
    const personSkill = person.skills.find(s => s.skillId === skill.id);
    return personSkill && personSkill.proficiencyLevel >= minProficiencyFilter;
  });

  return (
    <div className="p-6">
      {/* Skill header */}
      <div className="bg-white rounded-lg shadow-md p-6 mb-6">
        <div className="flex justify-between items-start">
          <div>
            <h1 className="text-3xl font-bold">{skill.name}</h1>
            <p className="text-gray-500">{skill.category}</p>
          </div>
          
          <div className="bg-blue-100 text-blue-800 text-sm font-medium rounded-full px-3 py-1.5">
            {allPeopleWithSkill.length} {allPeopleWithSkill.length === 1 ? 'person' : 'people'} with this skill
          </div>
        </div>
        
        <p className="mt-4 text-gray-700">
          {skill.description}
        </p>
      </div>
      
      {/* People with this skill */}
      <div className="flex justify-between items-center mb-6">
        <h2 className="text-xl font-semibold">People with this skill</h2>
        
        {/* Proficiency filter */}
        <div className="flex items-center">
          <span className="mr-3 text-sm text-gray-600">Minimum proficiency level:</span>
          <select 
            value={minProficiencyFilter}
            onChange={(e) => setMinProficiencyFilter(Number(e.target.value))}
            className="border border-gray-300 rounded px-2 py-1 text-sm"
          >
            <option value={1}>Novice (Level 1+)</option>
            <option value={2}>Beginner (Level 2+)</option>
            <option value={3}>Intermediate (Level 3+)</option>
            <option value={4}>Advanced (Level 4+)</option>
            <option value={5}>Expert (Level 5 only)</option>
          </select>
        </div>
      </div>
      
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {peopleWithSkill.map((person) => (
          <PersonCard
            key={person.id}
            person={person}
            onClick={() => onPersonClick(person.id)}
            onSkillClick={(skillId) => onPersonClick(skillId)} // Pass the skill click through to maintain proper navigation
          />
        ))}
      </div>
      
      {peopleWithSkill.length === 0 && (
        <div className="text-center p-8 text-gray-500">
          No people found with this skill.
        </div>
      )}
    </div>
  );
};

export default SkillScreen;
