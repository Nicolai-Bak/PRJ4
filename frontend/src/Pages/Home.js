import "./Home.css";
import ShoppingList from "../components/ShoppingList/ShoppingList";
import NewItemForm from "../components/NewItem/NewItemForm";
import { useState, useEffect } from "react";

function Home() {
	const initialShoppingList = localStorage.hasOwnProperty("shoppingList")
		? JSON.parse(localStorage.getItem("shoppingList"))
		: [];

	const [shoppingList, setShoppingList] = useState(initialShoppingList);

	useEffect(() => {
		if (localStorage.hasOwnProperty("shoppingList")) {
			console.log(
				"A change has been made to shoppingList, updating localStorage"
			);
			localStorage.setItem("shoppingList", JSON.stringify(shoppingList));
		}
	}, [shoppingList]);

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

	const removeItemHandler = (id, name) => {
		console.log(`removeItemHandler called with id: ${id} and name: ${name}`);
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.filter((item) => item.id !== id);
		});
	};

	const decimalController = (amount, change) => {
		amount = +amount;

		if (amount % 1 === 0) {
			return amount + change;
		}
		if (amount < 1) {
			return Number(+amount.toFixed(2) + change * 0.01).toFixed(2);
		}
		if (amount == amount.toFixed(1)) {
			console.log("1 decimal");
			return Number(+amount.toFixed(1) + +change / 10).toFixed(1);
		}
		return Number(amount + change / 100).toFixed(2);
	};

	const changeAmountHandler = (id, change) => {
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.map((item) => {
				if (item.id !== id || item.amount + change < 0) return item; //change can be positive or negative
				let oldAmount = +item.amount;

				if (item.unit !== "stk" && Math.round(item.amount) !== item.amount) {
					return {
						...item,
						amount: decimalController(oldAmount, +change),
					};
				} else {
					return {
						...item,
						amount: decimalController(oldAmount, +change),
					};
				}
			});
		});
	};

	const searchHandler = async () => {
		console.log(
			`searchHandler called with list: ${JSON.stringify(shoppingList)}`
		);
		// send amount, name, unit to search function
		// search function should return a list of items that matches the search
		// if no items are found, return an empty list

		const request = await fetch("https://prisninjawebapi.azurewebsites.net/option"); // is blocked by CORS
		const data = await request.json();
		console.log(data);

		// return list of items?
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
				onSearch={searchHandler}
				onRemoveItem={removeItemHandler}
				onAmountChanged={changeAmountHandler}
			/>
		</div>
	);
}

export default Home;
