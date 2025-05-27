import React from 'react';
import { Skill } from '../types';

interface SkillCardProps {
  skill: Skill;
  onClick?: () => void;
  peopleCount?: number; // Optional count of people with this skill
}

/**
 * A component that displays a card with information about a skill
 */
const SkillCard: React.FC<SkillCardProps> = ({ skill, onClick, peopleCount }) => {
  return (
    <div 
      className="bg-white rounded-lg shadow-md p-4 cursor-pointer hover:shadow-lg transition-shadow"
      onClick={onClick}
    >
      <div className="flex justify-between items-start">
        <div>
          <h3 className="text-lg font-semibold">
            {skill.name}
          </h3>
          <p className="text-gray-600 text-sm">{skill.category}</p>
        </div>
        
        {peopleCount !== undefined && (
          <div className="bg-blue-100 text-blue-800 text-xs font-medium rounded-full px-2.5 py-1">
            {peopleCount} {peopleCount === 1 ? 'person' : 'people'}
          </div>
        )}
      </div>
      
      <p className="mt-3 text-gray-700 text-sm">
        {skill.description}
      </p>
    </div>
  );
};

export default SkillCard;
