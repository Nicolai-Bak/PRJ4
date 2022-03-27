import "./App.css";
import ShoppingList from "./components/ShoppingList/ShoppingList";
import NewItemForm from "./components/NewItem/NewItemForm";
import NavBar from "./components/NavBar/NavBar";
import { useState } from "react";

function App() {
	const [shoppingList, setShoppingList] = useState([]);

	const newItemHandler = (item, amount, unit) => {
		console.log(
			`newItemHandler called with item: ${item}, amount: ${amount}, and unit: ${unit}`
		);

		setShoppingList((prevShoppingList) => {
			console.log("setting shopping list")
			return [
				...prevShoppingList,
				{
					name: item,
					amount: amount,
					unit: unit,
					id: Math.random().toString(), //<---- id needs to be changed
				},
			];
		});
	};

	const removeItemHandler = (id) => {
		console.log(`removeItemHandler called with id: ${id}`);
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.filter((item) => item.id !== id);
		});
	};

	const changeAmountHandler = (id, sign) => {
		console.log(`changeAmountHandler called with id: ${id}`);
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.map((item) => {
				if (item.id === id) {
					return {
						...item,
						amount: sign, // needs to decrease!!
					};
				}
			});
		});
	};


	return (
		<div className="App">
			<NavBar />
			<NewItemForm onItemAdded={newItemHandler} />
			<ShoppingList items={shoppingList} onRemoveItem={removeItemHandler} onAmountChanged={changeAmountHandler}/>
		</div>
	);
}

export default App;
