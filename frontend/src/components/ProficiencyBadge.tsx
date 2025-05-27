import React from 'react';
import { ProficiencyLevel, ProficiencyLevelLabel } from '../types';

interface ProficiencyBadgeProps {
  level: ProficiencyLevel;
  showLabel?: boolean;
  size?: 'sm' | 'md' | 'lg';
}

/**
 * A component that visually displays a skill proficiency level
 */
const ProficiencyBadge: React.FC<ProficiencyBadgeProps> = ({ 
  level, 
  showLabel = true, 
  size = 'md' 
}) => {
  // Determine colors based on proficiency level
  const getColorClass = () => {
    switch (level) {
      case ProficiencyLevel.Novice:
        return 'bg-gray-200 text-gray-800';
      case ProficiencyLevel.Beginner:
        return 'bg-blue-200 text-blue-800';
      case ProficiencyLevel.Intermediate:
        return 'bg-green-200 text-green-800';
      case ProficiencyLevel.Advanced:
        return 'bg-purple-200 text-purple-800';
      case ProficiencyLevel.Expert:
        return 'bg-yellow-200 text-yellow-800';
      default:
        return 'bg-gray-200 text-gray-800';
    }
  };

  // Determine size classes
  const getSizeClass = () => {
    switch (size) {
      case 'sm':
        return 'text-xs px-2 py-0.5';
      case 'md':
        return 'text-sm px-2.5 py-1';
      case 'lg':
        return 'text-base px-3 py-1.5';
      default:
        return 'text-sm px-2.5 py-1';
    }
  };

  // Render stars based on proficiency level
  const renderStars = () => {
    return (
      <div className="flex">
        {Array.from({ length: 5 }).map((_, i) => (
          <span key={i} className={`${i < level ? 'text-yellow-500' : 'text-gray-300'}`}>
            ★
          </span>
        ))}
      </div>
    );
  };

  return (
    <span className={`inline-flex items-center rounded-full font-medium ${getColorClass()} ${getSizeClass()}`}>
      {renderStars()}
      {showLabel && (
        <span className="ml-1">
          {ProficiencyLevelLabel[level]}
        </span>
      )}
    </span>
  );
};

export default ProficiencyBadge;
