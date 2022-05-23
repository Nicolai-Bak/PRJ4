
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

  import Component0 from '../src/Pages/Home.js';
  
reactComponents['Home'] = Component0;

import Component1 from '../src/components/ShoppingList/ShoppingListItem/ListItem.js';
reactComponents['ListItem'] = Component1;

import Component2 from '../src/components/NewItem/NewItemForm.js';
reactComponents['NewItemForm'] = Component2;

import Component3 from '../src/components/NewItem/SearchField.js';
reactComponents['SearchField'] = Component3;

import Component4 from '../src/components/ShoppingList/ShoppingList.js';
reactComponents['ShoppingList'] = Component4;

import Component5 from '../src/components/NewItem/UnitBox.js';
reactComponents['UnitBox'] = Component5;
