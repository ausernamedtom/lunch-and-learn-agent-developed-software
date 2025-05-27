import React, { useState, useEffect } from 'react';
import { Person, PersonSkill } from '../types';
import { getSkillById, getPersonById } from '../mocks/mockData';
import ProficiencyBadge from '../components/ProficiencyBadge';

interface DetailScreenProps {
  personId: string;
  onSkillClick: (skillId: string) => void;
}

/**
 * Detail Screen - Shows detailed information about a specific person
 */
const DetailScreen: React.FC<DetailScreenProps> = ({ personId, onSkillClick }) => {
  const [person, setPerson] = useState<Person | undefined>(getPersonById(personId));
  
  // Update person when personId changes
  useEffect(() => {
    setPerson(getPersonById(personId));
  }, [personId]);
  
  if (!person) {
    return <div className="p-6">Person not found</div>;
  }

  // Get skill name by ID
  const getSkillName = (skillId: string): string => {
    const skill = getSkillById(skillId);
    return skill ? skill.name : 'Unknown Skill';
  };

  // Get skill description by ID
  const getSkillDescription = (skillId: string): string => {
    const skill = getSkillById(skillId);
    return skill ? skill.description : '';
  };

  // Get skill category by ID
  const getSkillCategory = (skillId: string): string => {
    const skill = getSkillById(skillId);
    return skill ? skill.category : '';
  };

  // Handle skill click
  const handleSkillClick = (skillId: string) => {
    onSkillClick(skillId);
  };

  return (
    <div className="p-6">
      {/* Header */}
      <div className="flex items-center mb-6">
        {person.photoUrl && (
          <img 
            src={person.photoUrl} 
            alt={`${person.firstName} ${person.lastName}`}
            className="w-24 h-24 rounded-full object-cover mr-6"
          />
        )}
        
        <div>
          <h1 className="text-3xl font-bold">
            {person.firstName} {person.lastName}
          </h1>
          <p className="text-xl text-gray-600">{person.jobTitle}</p>
          <p className="text-gray-500">{person.department} • {person.email}</p>
        </div>
      </div>
      
      {/* Skills section */}
      <div className="bg-white rounded-lg shadow-md p-6 mb-6">
        <h2 className="text-xl font-semibold mb-4">Skills</h2>
        
        <div className="space-y-4">
          {person.skills.map((personSkill: PersonSkill) => (
            <div 
              key={personSkill.skillId} 
              className="border-b border-gray-100 last:border-b-0 pb-4 last:pb-0"
            >
              <div className="flex justify-between items-center mb-2">
                <div>
                  <button 
                    className="font-medium text-blue-600 hover:underline flex items-center"
                    onClick={() => handleSkillClick(personSkill.skillId)}
                  >
                    {getSkillName(personSkill.skillId)}
                    <svg className="w-3.5 h-3.5 ml-1" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                    </svg>
                  </button>
                  <p className="text-sm text-gray-500">{getSkillCategory(personSkill.skillId)}</p>
                </div>
                <div className="flex items-center">
                  <ProficiencyBadge level={personSkill.proficiencyLevel} />
                  {personSkill.isVerified && (
                    <span className="ml-2 text-green-600 text-sm flex items-center">
                      <svg className="w-4 h-4 mr-1" fill="currentColor" viewBox="0 0 20 20">
                        <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clipRule="evenodd"></path>
                      </svg>
                      Verified
                    </span>
                  )}
                </div>
              </div>
              
              <p className="text-sm text-gray-700">
                {getSkillDescription(personSkill.skillId)}
              </p>
              
              {personSkill.yearsOfExperience && (
                <p className="text-xs text-gray-500 mt-1">
                  {personSkill.yearsOfExperience} {personSkill.yearsOfExperience === 1 ? 'year' : 'years'} of experience
                </p>
              )}
            </div>
          ))}
        </div>
      </div>
      
      {/* Placeholder for additional sections */}
      <div className="bg-white rounded-lg shadow-md p-6">
        <h2 className="text-xl font-semibold mb-4">Projects</h2>
        <p className="text-gray-500 italic">
          This section would show projects the person has worked on. (Not implemented in this demo)
        </p>
      </div>
    </div>
  );
};

export default DetailScreen;
