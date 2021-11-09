import React, { Fragment, useState } from 'react';
import './EquipmentFinder.css';

export function EquipmentFinder() {
  const [searchText, setSearchText] = useState('');

  return (
    <Fragment>
      {/* Search box */}
      <div className="h-10 p-2 px-4 sticky top-16 z-10 bg-gray-700 text-white flex justify-between items-center">
        <h1>Equipment Finder</h1>
        <input type="search" value={ searchText } onChange={ e => setSearchText(e.target.value) }
               placeholder="Search..." className="search" />
      </div>

      {/* Inventory grid */}
      <inventory-grid search-name={ searchText } />
    </Fragment>
  );
}
