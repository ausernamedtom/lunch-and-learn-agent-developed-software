import React from 'react';
import ApiStatus from './ApiStatus';

interface NavbarProps {
  activePage: 'overview' | 'detail' | 'skill';
  onNavigate: (page: 'overview') => void;
  title: string;
  showBackButton?: boolean;
  onBack?: () => void;
}

/**
 * Navigation component for the application
 */
const Navbar: React.FC<NavbarProps> = ({ 
  activePage, 
  onNavigate, 
  title, 
  showBackButton = false, 
  onBack 
}) => {
  return (
    <nav className="bg-blue-600 text-white p-4">
      <div className="container mx-auto flex items-center justify-between">
        <div className="flex items-center">
          {showBackButton && onBack && (
            <button 
              className="mr-4 hover:text-blue-200 flex items-center"
              onClick={onBack}
            >
              <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 19l-7-7 7-7" />
              </svg>
              <span className="ml-1">Back</span>
            </button>
          )}
          <div className="font-bold text-xl">{title}</div>
        </div>
        
        <div className="flex items-center space-x-6">
          <ul className="flex space-x-6">
            <li>
              <button 
                className={`${activePage === 'overview' ? 'font-bold' : ''} hover:text-blue-200`}
                onClick={() => onNavigate('overview')}
              >
                People Overview
              </button>
            </li>
          </ul>
          
          <ApiStatus className="ml-4" />
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
