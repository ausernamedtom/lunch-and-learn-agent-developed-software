import React, { createContext, useContext, useEffect, useState, ReactNode } from 'react';
import { Person, Skill, ProficiencyLevel } from '../types';
import { ApiClient, ApiError } from '../services/ApiClient';
import { getPersonById, getSkillById, mockPeople, mockSkills } from '../mocks/mockData';

// Define the shape of our API context
interface ApiContextType {
  // Data
  people: Person[];
  skills: Skill[];
  loading: boolean;
  error: string | null;
  useBackend: boolean;
  
  // Actions
  toggleBackend: () => void;
  fetchPeople: (search?: string) => Promise<void>;
  fetchPerson: (id: string) => Promise<Person | undefined>;
  fetchSkills: (search?: string, category?: string) => Promise<void>;
  fetchSkill: (id: string) => Promise<Skill | undefined>;
  fetchPeopleWithSkill: (skillId: string, minProficiency?: ProficiencyLevel) => Promise<Person[]>;
  addPersonSkill: (personId: string, skillId: string, proficiencyLevel: ProficiencyLevel, yearsOfExperience?: number) => Promise<void>;
  updatePersonSkill: (personId: string, skillId: string, proficiencyLevel: ProficiencyLevel, yearsOfExperience?: number) => Promise<void>;
  removePersonSkill: (personId: string, skillId: string) => Promise<void>;
}

// Create the context with a default value
const ApiContext = createContext<ApiContextType>({
  people: [],
  skills: [],
  loading: false,
  error: null,
  useBackend: false,
  
  toggleBackend: () => {},
  fetchPeople: async () => {},
  fetchPerson: async () => undefined,
  fetchSkills: async () => {},
  fetchSkill: async () => undefined,
  fetchPeopleWithSkill: async () => [],
  addPersonSkill: async () => {},
  updatePersonSkill: async () => {},
  removePersonSkill: async () => {},
});

// Props for the ApiProvider component
interface ApiProviderProps {
  children: ReactNode;
}

// Provider component
export const ApiProvider: React.FC<ApiProviderProps> = ({ children }) => {
  const [people, setPeople] = useState<Person[]>([]);
  const [skills, setSkills] = useState<Skill[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [useBackend, setUseBackend] = useState(false);

  // Toggle between mock data and API
  const toggleBackend = () => {
    setUseBackend(prev => !prev);
  };

  // Initialize with mock data
  useEffect(() => {
    if (!useBackend) {
      setPeople(mockPeople);
      setSkills(mockSkills);
    } else {
      // Load initial data from API
      fetchPeople();
      fetchSkills();
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [useBackend]);

  // Fetch all people
  const fetchPeople = async (search?: string) => {
    if (!useBackend) {
      if (search) {
        const filteredPeople = mockPeople.filter(p => 
          p.firstName.toLowerCase().includes(search.toLowerCase()) ||
          p.lastName.toLowerCase().includes(search.toLowerCase()) ||
          p.jobTitle.toLowerCase().includes(search.toLowerCase()) ||
          p.department.toLowerCase().includes(search.toLowerCase())
        );
        setPeople(filteredPeople);
      } else {
        setPeople(mockPeople);
      }
      return;
    }

    setLoading(true);
    setError(null);
    
    try {
      const data = await ApiClient.getAllPeople(search);
      setPeople(data);
    } catch (err) {
      setError((err as ApiError).message || 'Failed to fetch people');
      console.error('Error fetching people:', err);
    } finally {
      setLoading(false);
    }
  };

  // Fetch a single person by ID
  const fetchPerson = async (id: string): Promise<Person | undefined> => {
    if (!useBackend) {
      return getPersonById(id);
    }

    setLoading(true);
    setError(null);
    
    try {
      const person = await ApiClient.getPersonById(id);
      return person;
    } catch (err) {
      setError((err as ApiError).message || `Failed to fetch person with ID: ${id}`);
      console.error(`Error fetching person ${id}:`, err);
      return undefined;
    } finally {
      setLoading(false);
    }
  };

  // Fetch all skills
  const fetchSkills = async (search?: string, category?: string) => {
    if (!useBackend) {
      if (search) {
        const filteredSkills = mockSkills.filter(s => 
          s.name.toLowerCase().includes(search.toLowerCase()) ||
          s.description.toLowerCase().includes(search.toLowerCase()) ||
          s.category.toLowerCase().includes(search.toLowerCase())
        );
        setSkills(filteredSkills);
      } else if (category) {
        const filteredSkills = mockSkills.filter(s => 
          s.category.toLowerCase() === category.toLowerCase()
        );
        setSkills(filteredSkills);
      } else {
        setSkills(mockSkills);
      }
      return;
    }

    setLoading(true);
    setError(null);
    
    try {
      const data = await ApiClient.getAllSkills(search, category);
      setSkills(data);
    } catch (err) {
      setError((err as ApiError).message || 'Failed to fetch skills');
      console.error('Error fetching skills:', err);
    } finally {
      setLoading(false);
    }
  };

  // Fetch a single skill by ID
  const fetchSkill = async (id: string): Promise<Skill | undefined> => {
    if (!useBackend) {
      return getSkillById(id);
    }

    setLoading(true);
    setError(null);
    
    try {
      const skill = await ApiClient.getSkillById(id);
      return skill;
    } catch (err) {
      setError((err as ApiError).message || `Failed to fetch skill with ID: ${id}`);
      console.error(`Error fetching skill ${id}:`, err);
      return undefined;
    } finally {
      setLoading(false);
    }
  };

  // Fetch people with a specific skill
  const fetchPeopleWithSkill = async (skillId: string, minProficiency?: ProficiencyLevel): Promise<Person[]> => {
    if (!useBackend) {
      const peopleWithSkill = mockPeople.filter(p => 
        p.skills.some(s => s.skillId === skillId && 
          (!minProficiency || s.proficiencyLevel >= minProficiency))
      );
      return peopleWithSkill;
    }

    setLoading(true);
    setError(null);
    
    try {
      const data = await ApiClient.getPeopleWithSkill(skillId, minProficiency);
      return data;
    } catch (err) {
      setError((err as ApiError).message || `Failed to fetch people with skill ID: ${skillId}`);
      console.error(`Error fetching people with skill ${skillId}:`, err);
      return [];
    } finally {
      setLoading(false);
    }
  };

  // Add a skill to a person
  const addPersonSkill = async (
    personId: string, 
    skillId: string, 
    proficiencyLevel: ProficiencyLevel, 
    yearsOfExperience?: number
  ) => {
    if (!useBackend) {
      // Update mock data
      const updatedPeople = people.map(person => {
        if (person.id === personId) {
          // Check if skill already exists
          const hasSkill = person.skills.some(s => s.skillId === skillId);
          if (hasSkill) return person;
          
          // Add new skill
          return {
            ...person,
            skills: [
              ...person.skills,
              {
                skillId,
                proficiencyLevel,
                yearsOfExperience,
                isVerified: false
              }
            ]
          };
        }
        return person;
      });
      
      setPeople(updatedPeople);
      return;
    }

    setLoading(true);
    setError(null);
    
    try {
      await ApiClient.addPersonSkill(personId, skillId, proficiencyLevel, yearsOfExperience);
      // Refresh the person data
      await fetchPerson(personId);
    } catch (err) {
      setError((err as ApiError).message || 'Failed to add skill');
      console.error('Error adding skill:', err);
    } finally {
      setLoading(false);
    }
  };

  // Update a person's skill
  const updatePersonSkill = async (
    personId: string, 
    skillId: string, 
    proficiencyLevel: ProficiencyLevel, 
    yearsOfExperience?: number
  ) => {
    if (!useBackend) {
      // Update mock data
      const updatedPeople = people.map(person => {
        if (person.id === personId) {
          return {
            ...person,
            skills: person.skills.map(skill => {
              if (skill.skillId === skillId) {
                return {
                  ...skill,
                  proficiencyLevel,
                  yearsOfExperience
                };
              }
              return skill;
            })
          };
        }
        return person;
      });
      
      setPeople(updatedPeople);
      return;
    }

    setLoading(true);
    setError(null);
    
    try {
      await ApiClient.updatePersonSkill(personId, skillId, proficiencyLevel, yearsOfExperience);
      // Refresh the person data
      await fetchPerson(personId);
    } catch (err) {
      setError((err as ApiError).message || 'Failed to update skill');
      console.error('Error updating skill:', err);
    } finally {
      setLoading(false);
    }
  };

  // Remove a skill from a person
  const removePersonSkill = async (personId: string, skillId: string) => {
    if (!useBackend) {
      // Update mock data
      const updatedPeople = people.map(person => {
        if (person.id === personId) {
          return {
            ...person,
            skills: person.skills.filter(skill => skill.skillId !== skillId)
          };
        }
        return person;
      });
      
      setPeople(updatedPeople);
      return;
    }

    setLoading(true);
    setError(null);
    
    try {
      await ApiClient.removePersonSkill(personId, skillId);
      // Refresh the person data
      await fetchPerson(personId);
    } catch (err) {
      setError((err as ApiError).message || 'Failed to remove skill');
      console.error('Error removing skill:', err);
    } finally {
      setLoading(false);
    }
  };

  // Create the value object for our context
  const contextValue = {
    people,
    skills,
    loading,
    error,
    useBackend,
    toggleBackend,
    fetchPeople,
    fetchPerson,
    fetchSkills,
    fetchSkill,
    fetchPeopleWithSkill,
    addPersonSkill,
    updatePersonSkill,
    removePersonSkill
  };

  return (
    <ApiContext.Provider value={contextValue}>
      {children}
    </ApiContext.Provider>
  );
};

// Custom hook for using the API context
export const useApi = () => useContext(ApiContext);
