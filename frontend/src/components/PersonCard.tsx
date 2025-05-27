import React from 'react';
import { Person } from '../types';
import { getPersonTopSkills } from '../mocks/mockData';
import ProficiencyBadge from './ProficiencyBadge';

interface PersonCardProps {
  person: Person;
  onClick?: () => void;
  onSkillClick?: (skillId: string) => void;
}

/**
 * A component that displays a card with basic information about a person
 */
const PersonCard: React.FC<PersonCardProps> = ({ person, onClick, onSkillClick }) => {
  const topSkills = getPersonTopSkills(person.id, 3);

  // Handle skill click without triggering the parent card click
  const handleSkillClick = (e: React.MouseEvent, skillId: string) => {
    e.stopPropagation();
    if (onSkillClick) {
      onSkillClick(skillId);
    }
  };

  return (
    <div 
      className="bg-white rounded-lg shadow-md p-4 cursor-pointer hover:shadow-lg transition-all duration-200 hover:translate-y-[-2px]"
      onClick={onClick}
    >
      <div className="flex items-center">
        {person.photoUrl && (
          <img 
            src={person.photoUrl} 
            alt={`${person.firstName} ${person.lastName}`}
            className="w-16 h-16 rounded-full object-cover mr-4"
          />
        )}
        
        <div>
          <h3 className="text-lg font-semibold">
            {person.firstName} {person.lastName}
          </h3>
          <p className="text-gray-600">{person.jobTitle}</p>
          <p className="text-gray-500 text-sm">{person.department}</p>
        </div>
      </div>
      
      <div className="mt-4">
        <h4 className="text-sm font-medium text-gray-700 mb-2">Top Skills:</h4>
        <div className="space-y-2">
          {topSkills.map((skillInfo) => (
            <div key={skillInfo.skill.id} className="flex items-center justify-between">
              <button 
                className="text-sm text-blue-600 hover:underline cursor-pointer flex items-center"
                onClick={(e) => handleSkillClick(e, skillInfo.skill.id)}
              >
                {skillInfo.skill.name}
                <svg className="w-3 h-3 ml-1" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
              </button>
              <ProficiencyBadge level={skillInfo.proficiency} size="sm" />
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default PersonCard;
