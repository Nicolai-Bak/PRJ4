import './App.css';
import ShoppingList from './components/ShoppingList/ShoppingList';
import NavBar from './components/NavBar/NavBar';

function App() {
  return (
    <div className="App">
        <NavBar/>
      <ShoppingList>
      </ShoppingList>
    </div>
  );
}

export default App;
