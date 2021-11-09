import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import { AlienCounter } from './AlienCounter.js';
import './NavMenu.css';

export function NavMenu() {
  const [expanded, setExpanded] = useState(false);
  const sidebarClass = `sidebar-bg z-10 fixed bg-black bg-opacity-60 inset-0 top-16 ${ expanded ? '' : 'hidden' }`;
  const sidebarContentClass = `sidebar-content transform top-16 bottom-0 w-96 left-0 bg-gray-900 p-2 fixed overflow-auto ease-in-out transition-all duration-300 z-30 flex flex-col ${ expanded ? 'translate-x-0' : '-translate-x-full' }`;
  return (
    <header className="top-bar bg-black text-white uppercase px-6 py-3 h-16 sticky top-0 z-50">
      <div className="text-2xl flex items-center">
        <button className="sidebar-toggler mr-6 p-2" onClick={ () => setExpanded(!expanded) }>
          <svg className="block h-6 w-6" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M4 6h16M4 12h16M4 18h16" />
          </svg>
        </button>
        <h1>Mission Control</h1>
        <AlienCounter />
      </div>

      <div className={ sidebarClass } style={{ top: '4rem' }} onClick={ () => setExpanded(!expanded) }></div>
      <div className={ sidebarContentClass } onClick={ () => setExpanded(!expanded) }>
        <LoginMenu />
        <div className="text-2xl">
          <NavLink className="block m-4 py-4 px-6 bg-gray-800" to="/" exact>Mission Status</NavLink>
          <NavLink className="block m-4 py-4 px-6 bg-gray-800" to="/future" exact>Planning</NavLink>
          <NavLink className="block m-4 py-4 px-6 bg-gray-800" to="/broadcasts" exact>Broadcasts</NavLink>
          <NavLink className="block m-4 py-4 px-6 bg-gray-800" to="/safety" exact>Safety Checklists</NavLink>
          <NavLink className="block m-4 py-4 px-6 bg-gray-800" to="/equipment">Equipment Finder</NavLink>
        </div>

        <div className="mt-auto p-3 m-5 bg-black bg-opacity-80 rounded-2xl flex items-center justify-center">
          Built using
          <img src="/react-logo.svg" className="inline-block w-12" alt="React logo" />
          React.js
        </div>
      </div>
    </header>
);
}
