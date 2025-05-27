import React from 'react';
import { useApi } from '../contexts/ApiContext';

interface ApiStatusProps {
  className?: string;
}

const ApiStatus: React.FC<ApiStatusProps> = ({ className }) => {
  const { useBackend, toggleBackend, loading } = useApi();

  return (
    <div className={`flex items-center ${className || ''}`}>
      <span className="mr-2 text-sm">API:</span>
      
      {/* Status indicator */}
      <div className="relative inline-flex items-center">
        <div 
          className={`h-3 w-3 rounded-full mr-2 ${
            loading 
              ? 'bg-yellow-400' 
              : useBackend 
                ? 'bg-green-500' 
                : 'bg-gray-400'
          }`}
        />
        <span className="text-sm">
          {loading ? 'Loading...' : useBackend ? 'Connected' : 'Mock Data'}
        </span>
      </div>
      
      {/* Toggle button */}
      <button
        onClick={toggleBackend}
        className="ml-3 px-2 py-1 text-xs rounded bg-blue-100 hover:bg-blue-200 text-blue-800 transition-colors"
        disabled={loading}
      >
        {useBackend ? 'Use Mock Data' : 'Use API'}
      </button>
    </div>
  );
};

export default ApiStatus;
