import "./App.css";
import ShoppingList from "./components/ShoppingList/ShoppingList";
import ShoppingOption from "./components/SearchResults/ShoppingOption";
import NewItemForm from "./components/NewItem/NewItemForm";
import NavBar from './components/NavBar/NavBar';
import { useState } from "react";

function App() {
	const [shoppingList, setShoppingList] = useState([]);

	const newItemHandler = (item, amount) => {
		console.log(
			`newItemHandler called with item: ${item} and amount: ${amount}`
		);

		setShoppingList((prevShoppingList) => {
			return [
				...prevShoppingList,
				{
					name: item,
					amount: amount,
					id: Math.random().toString(), //<---- id needs to be changed
				},
			];
		});
	};

	return (
		<div className="App">
      <NavBar/>
			<NewItemForm onItemAdded={newItemHandler} />
      <ShoppingList items={shoppingList}/>
		</div>
	);
}

export default App;
