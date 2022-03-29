import "./Home.css";
import ShoppingList from "../components/ShoppingList/ShoppingList";
import NewItemForm from "../components/NewItem/NewItemForm";
import { useState, useEffect } from "react";

function Home() {
	const initialShoppingList = localStorage.hasOwnProperty("shoppingList")
		? JSON.parse(localStorage.getItem("shoppingList"))
		: [];
	
	console.log("InitialShoppingList: " + initialShoppingList);
	const [shoppingList, setShoppingList] = useState(initialShoppingList);

	console.log(shoppingList);
	useEffect(() => {
		if (localStorage.hasOwnProperty("shoppingList")) {
			const tempList = localStorage.getItem(
				"shoppingList",
				JSON.stringify(shoppingList)
			);
			console.log(
				"A change has been made to shoppingList. Before: " + tempList
			);

			localStorage.setItem("shoppingList", JSON.stringify(shoppingList));
			console.log(
				"Now: " + localStorage.getItem("shoppingList")
			);
		}
	}, [shoppingList]);

	const newItemHandler = (item, amount, unit, id) => {
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
					id: Math.random() * 12, //<---- id needs to be changed
					key: Math.random() * 21,
				},
			];
		});

		localStorage.setItem("shoppingList", JSON.stringify(shoppingList));
		console.log(
			"Local Storage now contains: " + localStorage.getItem("shoppingList")
		);
	};

	const removeItemHandler = (id) => {
		console.log(`removeItemHandler called with id: ${id}`);
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.filter((item) => item.id !== id);
		});
	};

	const changeAmountHandler = (id, change) => {
		console.log(
			`changeAmountHandler called with id: ${id} and change: ${change}`
		);
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.map((item) => {
				if (item.id === id) {
					let oldAmount = +item.amount;
					return {
						...item,
						amount: (oldAmount + change).toFixed(2),
					};
				} else {
					return item;
				}
			});
		});
	};
	return (
		<div className="home">
			<div className="slogan__container">
				<div className="slogan">
					<img id="ninja-landing" src="/images/ninja-landing.svg" />
					<i>Find tilbuddene, før din nabo gør det!</i>
					<img id="ninja-rightside" src="/images/ninja-about.svg" />
				</div>
			</div>
			<NewItemForm onItemAdded={newItemHandler} />
			<ShoppingList
				items={shoppingList}
				onRemoveItem={removeItemHandler}
				onAmountChanged={changeAmountHandler}
			/>
		</div>
	);
}

export default Home;
