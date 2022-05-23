
    window.reactComponents = {};

    window.vueComponents = {};

  
      import React from "react";

      import ReactDOM from "react-dom";


      import ReactWrapper from '../node_modules/better-docs/lib/react-wrapper.js';

      window.React = React;

      window.ReactDOM = ReactDOM;

      window.ReactWrapper = ReactWrapper;

    
    import './styles/reset.css';

    import './styles/iframe.css';

  import Component0 from '../src/components/UI/Atoms/Button/Button.js';
reactComponents['Button'] = Component0;

import Component1 from '../src/components/Dropdown/Dropdown.js';
reactComponents['Dropdown'] = Component1;

import Component2 from '../src/components/UI/Organisms/Footer/Footer.js';
reactComponents['Footer'] = Component2;

import Component3 from '../src/components/UI/Organisms/NavBar/NavBar.js';
reactComponents['NavBar'] = Component3;

import Component4 from '../src/components/NewItem/NewItemForm.js';
reactComponents['NewItemForm'] = Component4;

import Component5 from '../src/components/NewItem/SearchField.js';
reactComponents['SearchField'] = Component5;

import Component6 from '../src/components/SearchResult/SearchResult.js';
reactComponents['SearchResult'] = Component6;

import Component7 from '../src/components/SearchResult/SearchResultItem/SearchResultItem.js';
reactComponents['SearchResultItem'] = Component7;

import Component8 from '../src/Pages/SearchResultsPage.js';
reactComponents['SearchResultsPage'] = Component8;

import Component9 from '../src/components/NewItem/UnitBox.js';
reactComponents['UnitBox'] = Component9;