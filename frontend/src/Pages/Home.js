import "./Home.css";
import ShoppingList from "../components/ShoppingList/ShoppingList";
import NewItemForm from "../components/NewItem/NewItemForm";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Banner from "../components/Banner/Banner";
import DuplicateDialog from "../components/ShoppingList/DuplicateDialog";

function Home() {
	const initialShoppingList = localStorage.hasOwnProperty("shoppingList")
		? JSON.parse(localStorage.getItem("shoppingList"))
		: [];

	let navigate = useNavigate();
	// DuplicateDialog states
	const [open, setOpen] = useState(false);
	const [amountToChange, setAmountToChange] = useState(null);
	const [existingItem, setExistingItem] = useState({
		name: "",
		amount: 0,
		unit: "",
		id: "",
		organic: false,
	});

	const [shoppingList, setShoppingList] = useState(initialShoppingList);

	useEffect(() => {
		if (localStorage.hasOwnProperty("shoppingList")) {
			console.log("updating localStorage...");
			localStorage.setItem("shoppingList", JSON.stringify(shoppingList));
		}
	}, [shoppingList]);

	const handleItemUpdate = (id, amount, unit, adding) => {
		// console.log(
		// 	"handleItemUpdate: ",
		// 	"id: ",
		// 	id.toString().slice(0, 5),
		// 	"value: ",
		// 	amount,
		// 	"unit :",
		// 	unit
		// );
		setShoppingList((prevShoppingList) => {
			return prevShoppingList.map((item) => {
				if (item.id !== id || amount < 0) return item;
				if (unit && !adding) {
					return {
						...item,
						unit: unit,
						amount: amount,
					};
				} else if (!adding) {
					return {
						...item,
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

	const handleDialogAdd = (itemName, amount) => {
		//find shoppinglist item with itemName
		const item = shoppingList.find((item) => item.name === itemName);
		if (item) {
			handleItemUpdate(item.id, +item.amount + +amount, item.unit, true);
		}
		handleClose();
	};

	const handleClose = (action) => {
		setOpen(false);
	};

	const newItemHandler = async (name, amount, unit, id, organic) => {
		// If an item with the same name exists on the shopping list already

		const existingItem = shoppingList.find(
			(item) => item.name.toLowerCase() === name.toLowerCase()
		);
		if (existingItem) {
			// console.log("Item already exists on the shopping list : ", existingItem);
			setExistingItem(existingItem);
			setAmountToChange(amount);
			setOpen(true);
			return;
		}

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

		// Sending the item information that is relevant to the database with the naming scheme that they are working with
		shoppingList.forEach((item) => {
			console.log(item);
			const itemDTO = {
				Name: item.name,
				Unit: item.amount,
				Measurement: item.unit,
				Organic: item.organic,
			};
			// console.log(itemDTO);
			searchList.push(itemDTO);
		});

		// searchList.forEach((item) => console.log(item));
		let request = false;
		try {
			request = await fetch(
				"https://prisninjawebapi.azurewebsites.net/options/ ",
				{
					method: "POST",
					headers: {
						Accept: "application/json",
						"Content-Type": "application/json",
					},
					body: JSON.stringify({
						products: searchList,
						Range: 50,
						y: latitude,
						x: longitude,
					}),
				}
			);
		} catch (error) {
			console.log(error);
			return;
		}

		console.log("request received: " + request);

		const data = await request.json();

		console.log("Data received from database: " + JSON.stringify(data));
		localStorage.setItem("SearchResults", JSON.stringify(data));

		navigate("/SearchResults");
	};

	//GEOLOCATION
	const [longitude, setLongitude] = useState(null);
	const [latitude, setLatitude] = useState(null);

	useEffect(() => {
		if (!navigator.geolocation) {
			console.log("Fejl i geolokation");
		} else {
			navigator.geolocation.getCurrentPosition((position) => {
				setLatitude(position.coords.latitude);
				setLongitude(position.coords.longitude);
				// console.log(`latitude: ${latitude}, longitude: ${longitude}`);
			});
		}
	}, [longitude, latitude]);

	const onAddDialog = (event) => {
		setOpen(false);
		handleDialogAdd(existingItem.name, amountToChange);
	};

	return (
		<div className="home">
			<Banner />
			<div className="home-shopping-list-wrapper-upper">
				<div className="instructions-step-one">
					<div className="instructions-step-one-text">
						Trin 1: Søg efter din varer her!
					</div>
					<br />
					<img
						src="/images/arrow.svg"
						alt="arrow"
						className="instructions-arrow-one"
					/>
				</div>
				<NewItemForm
					className="home-new-item-form"
					onItemAdded={newItemHandler}
				/>
				<DuplicateDialog
					itemName={existingItem.name}
					amount={amountToChange}
					unit={existingItem.unit}
					existingAmount={existingItem.amount}
					onCancel={handleClose}
					addAmount={onAddDialog}
					open={open}
				/>
				<div className="filler" />
			</div>
			<div className="home-shopping-list-wrapper-lower">
				<div className="instructions-step-three">
					<div className="instructions-step-three-wrapper">
						<div className="instructions-step-three-text">
							Trin 3: Tryk 'søg' og <br />
							se hvor du skal handle!
						</div>
						<br />
						<img
							src="/images/arrow.svg"
							alt="arrow"
							className="instructions-arrow-three"
						/>
					</div>
				</div>

				<ShoppingList
					className="home-shopping-list"
					items={shoppingList}
					onSearch={searchHandler}
					onRemoveItem={removeItemHandler}
					onAmountChanged={changeAmountHandler}
					onNewUnitOrAmount={handleItemUpdate}
				/>
				<div className="instructions-step-two">
					<div className="instructions-step-two-text">
						Trin 2: Dine varer tilføjes <br />
						herefter til din indkøbsliste
					</div>
					<br />
					<img
						src="/images/arrow.svg"
						alt="arrow"
						className="instructions-arrow-two"
					/>
				</div>
			</div>
		</div>
	);

	async function ValidateItem(name, unit) {
		const itemNames = JSON.parse(localStorage.getItem("itemNames"));

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
			matchFound = true;
		}
		return matchFound;
	}
}
export default Home;
