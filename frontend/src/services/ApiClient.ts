import { Person, Skill, ProficiencyLevel, PersonSkill } from '../types';

// API base URL
const API_BASE_URL = 'https://localhost:7294/api';

// Error type for API errors
export interface ApiError {
  status: number;
  message: string;
}

// HATEOAS links type
export interface ApiLinks {
  [key: string]: string;
}

// API client for the Skill Management API
export class ApiClient {
  /**
   * Generic fetch method with error handling
   */
  private static async fetchJson<T>(url: string, options?: RequestInit): Promise<T> {
    try {
      const response = await fetch(url, {
        ...options,
        headers: {
          ...options?.headers,
          'Content-Type': 'application/json',
          'Accept': 'application/json'
        }
      });

      if (!response.ok) {
        const errorData = await response.json().catch(() => ({}));
        throw {
          status: response.status,
          message: errorData.message || `HTTP error ${response.status}`
        } as ApiError;
      }

      return await response.json() as T;
    } catch (error) {
      if ((error as ApiError).status) {
        throw error;
      }
      throw {
        status: 0,
        message: (error as Error).message || 'Network error'
      } as ApiError;
    }
  }

  /**
   * Get API root with HATEOAS links
   */
  static async getApiRoot(): Promise<ApiLinks> {
    return await this.fetchJson<ApiLinks>(`${API_BASE_URL}`);
  }

  /**
   * Get all people
   */
  static async getAllPeople(search?: string): Promise<Person[]> {
    const url = search 
      ? `${API_BASE_URL}/people?search=${encodeURIComponent(search)}`
      : `${API_BASE_URL}/people`;
    return await this.fetchJson<Person[]>(url);
  }

  /**
   * Get person by ID
   */
  static async getPersonById(id: string): Promise<Person> {
    return await this.fetchJson<Person>(`${API_BASE_URL}/people/${id}`);
  }

  /**
   * Get all skills
   */
  static async getAllSkills(search?: string, category?: string): Promise<Skill[]> {
    let url = `${API_BASE_URL}/skills`;
    
    if (search) {
      url += `?search=${encodeURIComponent(search)}`;
    } else if (category) {
      url += `?category=${encodeURIComponent(category)}`;
    }
    
    return await this.fetchJson<Skill[]>(url);
  }

  /**
   * Get skill by ID
   */
  static async getSkillById(id: string): Promise<Skill> {
    return await this.fetchJson<Skill>(`${API_BASE_URL}/skills/${id}`);
  }

  /**
   * Get people with a specific skill
   */
  static async getPeopleWithSkill(skillId: string, minProficiency?: ProficiencyLevel): Promise<Person[]> {
    let url = `${API_BASE_URL}/skills/${skillId}/people`;
    
    if (minProficiency) {
      url += `?minProficiency=${minProficiency}`;
    }
    
    return await this.fetchJson<Person[]>(url);
  }

  /**
   * Add a new skill to a person
   */
  static async addPersonSkill(personId: string, skillId: string, proficiencyLevel: ProficiencyLevel, yearsOfExperience?: number): Promise<PersonSkill> {
    return await this.fetchJson<PersonSkill>(
      `${API_BASE_URL}/people/${personId}/skills`,
      {
        method: 'POST',
        body: JSON.stringify({
          skillId,
          proficiencyLevel,
          yearsOfExperience
        })
      }
    );
  }

  /**
   * Update a person's skill
   */
  static async updatePersonSkill(personId: string, skillId: string, proficiencyLevel: ProficiencyLevel, yearsOfExperience?: number): Promise<PersonSkill> {
    return await this.fetchJson<PersonSkill>(
      `${API_BASE_URL}/people/${personId}/skills/${skillId}`,
      {
        method: 'PUT',
        body: JSON.stringify({
          skillId,
          proficiencyLevel,
          yearsOfExperience
        })
      }
    );
  }

  /**
   * Remove a skill from a person
   */
  static async removePersonSkill(personId: string, skillId: string): Promise<void> {
    await fetch(
      `${API_BASE_URL}/people/${personId}/skills/${skillId}`,
      { method: 'DELETE' }
    );
  }
}
