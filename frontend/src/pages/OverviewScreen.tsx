import React, { useState } from 'react';
import { mockPeople, getSkillById } from '../mocks/mockData';
import PersonCard from '../components/PersonCard';
import { Person } from '../types';

interface OverviewScreenProps {
  onPersonClick: (personId: string) => void;
  onSkillClick: (skillId: string) => void;
}

/**
 * Overview Screen - Main entry point showing a list of all people
 */
const OverviewScreen: React.FC<OverviewScreenProps> = ({ onPersonClick, onSkillClick }) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [people] = useState<Person[]>(mockPeople);
  const [searchType, setSearchType] = useState<'all' | 'people' | 'skills'>('all');
  
  // Filter people based on search term and search type
  const filteredPeople = people.filter(person => {
    // Basic person info search
    const personMatches = 
      person.firstName.toLowerCase().includes(searchTerm.toLowerCase()) ||
      person.lastName.toLowerCase().includes(searchTerm.toLowerCase()) ||
      person.jobTitle.toLowerCase().includes(searchTerm.toLowerCase()) ||
      person.department.toLowerCase().includes(searchTerm.toLowerCase());
      
    // Skills search
    const skillMatches = person.skills.some(personSkill => {
      const skill = getSkillById(personSkill.skillId);
      return skill !== undefined && skill.name.toLowerCase().includes(searchTerm.toLowerCase());
    });
    
    if (searchType === 'people') return personMatches;
    if (searchType === 'skills') return skillMatches;
    return personMatches || skillMatches; // 'all' option
  });

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-6">Team Skills Overview</h1>
      
      {/* Search inputs */}
      <div className="mb-6">
        <div className="flex">
          <input
            type="text"
            placeholder="Search people and skills..."
            className="flex-1 rounded-l-lg border border-gray-300 px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
          <select
            value={searchType}
            onChange={(e) => setSearchType(e.target.value as 'all' | 'people' | 'skills')}
            className="rounded-r-lg border-t border-r border-b border-gray-300 px-3 py-2 bg-gray-50"
          >
            <option value="all">All</option>
            <option value="people">People</option>
            <option value="skills">Skills</option>
          </select>
        </div>
        {searchType === 'skills' && searchTerm && (
          <div className="mt-2 text-sm text-gray-600">
            Searching for people with skills matching: "{searchTerm}"
          </div>
        )}
      </div>
      
      {/* People list */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {filteredPeople.map((person) => (
          <PersonCard
            key={person.id}
            person={person}
            onClick={() => onPersonClick(person.id)}
            onSkillClick={onSkillClick}
          />
        ))}
      </div>
      
      {filteredPeople.length === 0 && (
        <div className="text-center p-8 text-gray-500">
          No people found matching your search criteria.
        </div>
      )}
    </div>
  );
};

export default OverviewScreen;
