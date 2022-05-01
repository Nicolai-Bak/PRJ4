import "./Home.css";
import ShoppingList from "../components/ShoppingList/ShoppingList";
import NewItemForm from "../components/NewItem/NewItemForm";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Banner from "../components/Banner/Banner";

function Home() {
	const initialShoppingList = localStorage.hasOwnProperty("shoppingList")
		? JSON.parse(localStorage.getItem("shoppingList"))
		: [];

	let navigate = useNavigate();

	const [shoppingList, setShoppingList] = useState(initialShoppingList);

	useEffect(() => {
		if (localStorage.hasOwnProperty("shoppingList")) {
			console.log(
				"A change has been made to shoppingList, updating localStorage..."
			);
			localStorage.setItem("shoppingList", JSON.stringify(shoppingList));
		}
	}, [shoppingList]);

	const handleItemUpdate = (id, amount, unit) => {
		console.log(
			"handleItemUpdate: ",
			"id: ",
			id.toString().slice(0, 5),
			"value: ",
			amount,
			"unit :",
			unit
		);
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.map((item) => {
				if (item.id !== id || amount < 0) return item;

				if (unit !== null) {
					console.log("success");
					return {
						...item,
						unit: unit,
						amount: amount,
					};
				} else {
					return {
						...item,
						amount: amount,
					};
				}
			});
		});
	};

	const newItemHandler = async (name, amount, unit, id, organic) => {
		console.log(
			`newItemHandler called with item: ${name}, amount: ${amount}, unit: ${unit}, id: ${
				id.toString().slice(0, 5) + "..."
			}, organic: ${organic}`
		);
		// if the item doesn't exists in the database
		if (!(await ValidateItem(name, unit))) {
			console.log("item not found in database");
			alert("Varen kan ikke genkendes");
			return;
		}

		setShoppingList((prevShoppingList) => {
			return [
				...prevShoppingList,
				{
					name: name,
					amount: amount,
					unit: unit,
					id: id, //uuid()
					key: id,
					organic: organic,
				},
			];
		});

		localStorage.setItem("shoppingList", JSON.stringify(shoppingList));
		// console.log(
		// 	"Local Storage now contains: " + localStorage.getItem("shoppingList")
		// );
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

		// const searchList = shoppingList.map((item) => item);
		// console.log("searchlist: " + JSON.stringify(searchList));
		const searchList = [];

		shoppingList.forEach((item) => {
			console.log(item);
			const itemDTO = {
				name: item.name,
				amount: item.amount,
				unit: item.unit,
				organic: item.organic,
			};
			console.log(itemDTO);
			searchList.push(itemDTO);
		});

		searchList.forEach((item) => console.log(item));
		const request = await fetch("https://localhost/options/", {
			method: "POST",
			headers: {
				Accept: "application/json",
				"Content-Type": "application/json",
			},
			body: JSON.stringify({
				products: searchList,
				y: latitude,
				x: longitude,
			}),
		});

		console.log("request received: " + request);

		const data = await request.json();

		console.log("Data received from database: " + JSON.stringify(data));
		localStorage.setItem("SearchResults", JSON.stringify(data));

		navigate("/SearchResults");
	};

	//GEOLOCATION
	const [longitude, setLongitude] = useState(null);
	const [latitude, setLatitude] = useState(null);
	const [status, setStatus] = useState(null);

	useEffect(() => {
		if (!navigator.geolocation) {
			setStatus("Geolokation understøttes ikke af din browser");
		} else {
			navigator.geolocation.getCurrentPosition((position) => {
				setStatus(null);
				setLatitude(position.coords.latitude);
				setLongitude(position.coords.longitude);
			});
		}
		console.log(`latitude: ${latitude}, longitude: ${longitude}`);
	}, []);

	return (
		<div className="home">
			<Banner />
			<NewItemForm onItemAdded={newItemHandler} />
			<ShoppingList
				items={shoppingList}
				onSearch={searchHandler}
				onRemoveItem={removeItemHandler}
				onAmountChanged={changeAmountHandler}
				onNewUnitOrAmount={handleItemUpdate}
			/>
		</div>
	);

	async function ValidateItem(name, unit) {
		const itemNames = JSON.parse(localStorage.getItem("itemNames"));

		console.log("itemNames: " + itemNames);

		let matchFound = false;
		let foundItems = [];
		if (itemNames.length < 1) {
			console.log("No items in localStorage");
			return;
		}

		itemNames.forEach((item) => {
			if (item.toLowerCase().includes(name.toLowerCase())) {
				// this needs to be uncommented when a unit is received

				// if (item.unit !== unit) {
				// 	console.log(`unit not found, received: ${item.unit}`);
				// 	return;
				// }
				foundItems.push(item);
				console.log("unit found");
			}
		});
		if (foundItems.length > 0) {
			console.log("items found: " + foundItems);
			matchFound = true;
		}
		return matchFound;
	}
}
export default Home;
