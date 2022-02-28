import './App.css';
import ShoppingList from './components/ShoppingList/ShoppingList';
import ShoppingOption from './components/SearchResults/ShoppingOption';

function App() {
  return (
    <div className="App">
      <ShoppingList>
      </ShoppingList>
      <ShoppingOption/>
    </div>
  );
}

export default App;
