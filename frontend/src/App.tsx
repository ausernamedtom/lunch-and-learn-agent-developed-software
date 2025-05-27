import React, { useState } from 'react';
import './App.css';
import Navbar from './components/Navbar';
import OverviewScreen from './pages/OverviewScreen';
import DetailScreen from './pages/DetailScreen';
import SkillScreen from './pages/SkillScreen';
import { ApiProvider } from './contexts/ApiContext';

function App() {
  // Simple state-based routing
  const [activePage, setActivePage] = useState<'overview' | 'detail' | 'skill'>('overview');
  const [selectedPersonId, setSelectedPersonId] = useState<string | null>(null);
  const [selectedSkillId, setSelectedSkillId] = useState<string | null>(null);
  
  // Navigation handlers
  const navigateToOverview = () => {
    setActivePage('overview');
    setSelectedPersonId(null);
    setSelectedSkillId(null);
  };

  const navigateToPersonDetail = (personId: string) => {
    setSelectedPersonId(personId);
    setActivePage('detail');
  };

  const navigateToSkill = (skillId: string) => {
    setSelectedSkillId(skillId);
    setActivePage('skill');
  };

  // Get the title for the current page
  const getPageTitle = () => {
    switch (activePage) {
      case 'overview':
        return 'Skill Management';
      case 'detail':
        return 'Person Profile';
      case 'skill':
        return 'Skill Details';
      default:
        return 'Skill Management';
    }
  };

  // Render the active page
  const renderActivePage = () => {
    switch (activePage) {
      case 'overview':
        return (
          <OverviewScreen 
            onPersonClick={navigateToPersonDetail} 
            onSkillClick={navigateToSkill} 
          />
        );
      case 'detail':
        return (
          <DetailScreen 
            personId={selectedPersonId || 'person-1'} 
            onSkillClick={navigateToSkill} 
          />
        );
      case 'skill':
        return (
          <SkillScreen 
            skillId={selectedSkillId || 'skill-1'} 
            onPersonClick={navigateToPersonDetail} 
          />
        );
      default:
        return <OverviewScreen 
          onPersonClick={navigateToPersonDetail} 
          onSkillClick={navigateToSkill} 
        />;
    }
  };

  return (
    <ApiProvider>
      <div className="min-h-screen bg-gray-50">
        <Navbar 
          activePage={activePage} 
          onNavigate={navigateToOverview} 
          title={getPageTitle()}
          showBackButton={activePage !== 'overview'}
          onBack={navigateToOverview}
        />
        <main className="container mx-auto py-4">
          {renderActivePage()}
        </main>
      </div>
    </ApiProvider>
  );
}

export default App;
