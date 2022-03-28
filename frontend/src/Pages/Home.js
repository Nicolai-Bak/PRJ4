import "./Home.css";
import ShoppingList from "../components/ShoppingList/ShoppingList"
import NewItemForm from "../components/NewItem/NewItemForm";
import { useState } from "react";

function Home() {
	
	const [shoppingList, setShoppingList] = useState([]);

	const newItemHandler = (item, amount, unit) => {
		console.log(
			`newItemHandler called with item: ${item}, amount: ${amount}, and unit: ${unit}`
		);

		setShoppingList((prevShoppingList) => {
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

	const changeAmountHandler = (id, change) => {
		console.log(`changeAmountHandler called with id: ${id} and change: ${change}`);
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.map((item) => {
				if (item.id === id) {
					let oldAmount = +item.amount;
					return {
						...item,
						amount: (oldAmount + change).toFixed(2),
					};
				} else return item;
			});
		});
	};
	return (
		<div className="home">
			<div className="slogan__container">
			<div className="slogan">
				<img id="ninja-landing" src="/images/ninja-landing.svg"/>
				<i>Find tilbuddene, før din nabo gør det!</i>
				<img id="ninja-rightside" src="/images/ninja-about.svg"/>
			</div>
			</div>
			<NewItemForm onItemAdded={newItemHandler} 
			/>
			<ShoppingList items={shoppingList}
				onRemoveItem={removeItemHandler}
				onAmountChanged={changeAmountHandler}
	  />
		</div>
	);
}

export default Home;