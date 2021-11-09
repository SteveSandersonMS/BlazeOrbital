import React, { useState } from 'react';

export function AlienCounter() {
  // Declare a new state variable, which we'll call "count"
  const [count, setCount] = useState(0);

  const digit1 = Math.floor(count / 10);
  const digit2 = count % 10;

  return (
    <div className="ml-auto flex">
      <span className="bg-yellow-200 text-black py-1 px-3 rounded mr-1">{ digit1 }</span>
      <span className="bg-yellow-200 text-black py-1 px-3 rounded mr-1">{ digit2 }</span>
      <span className="inline-flex flex-col ml-2">
        <span className="text-sm">days since last</span>
        <span className="text-sm">alien sighting</span>
      </span>
      <button onClick={ () => setCount(count + 1) } className="ml-3 opacity-60 hover:opacity-100 active:opacity-50" title="Increase">
        <svg className="h-6" viewBox="0 0 100 100" style={{ stroke: 'white' }}>
          <circle cx="50" cy="50" r="45" fill="none" strokeWidth="7.5"></circle>
          <line x1="32.5" y1="50" x2="67.5" y2="50" strokeWidth="5"></line>
          <line x1="50" y1="32.5" x2="50" y2="67.5" strokeWidth="5"></line>
        </svg>
      </button>
    </div>
  );
}
