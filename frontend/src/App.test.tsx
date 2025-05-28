import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders Skill Management title', () => {
  render(<App />);
  const titleElement = screen.getByText(/Skill Management/i);
  expect(titleElement).toBeInTheDocument();
});
