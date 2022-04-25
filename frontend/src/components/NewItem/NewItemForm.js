import "./NewItemForm.css";
import UnitBox from "./UnitBox";
import React, { useState, useEffect } from "react";
import { v4 as uuid } from "uuid";
import SearchField from "./SearchField";
import Card from "../UI/Atoms/Card/Card";
import Button from "../UI/Atoms/Button/Button";

const NewItemForm = (props) => {
	const [newItem, setNewItem] = useState(null);
	const [amount, setAmount] = useState("");
	const [unit, setUnit] = useState("kg");
	// changed validInput
	const [isSearchFieldValid, SetIsSearchFieldValid] = useState(true);
	const [isAmountValid, SetIsAmountValid] = useState(true);
	const id = uuid();

	useEffect(() => {
		fetchItems();
	}, []);

	const submitItemHandler = (event) => {
		event.preventDefault();
		// console.log(`You just tried to add ${amount} ${unit} ${newItem}'s`);

		if (validInput(newItem, amount)) {
			props.onItemAdded(newItem, amount, unit, id);
		} else return;

		// setNewItem(""); <-- doesn't reset the autocomplete field after submit
		setAmount("");
	};

	const itemChangeHandler = (itemReceived) => {
		SetIsSearchFieldValid(true);
		setNewItem(itemReceived);
		console.log(props);

		return fetchItemInfo(itemReceived);
	};

	const amountChangeHandler = (event) => {
		const added = event.target.value;
		added < 0 ? SetIsAmountValid(false) : setAmount(added);
		SetIsAmountValid(true);
	};

	const validInput = (newItem, amount) => {
		if (newItem === null || !newItem.length > 0) {
			SetIsSearchFieldValid(false);
			console.log("No item was detected in the input field");
			return false;
		}
		if (amount === null || !amount.length > 0) {
			SetIsAmountValid(false);
		}
		if (!amount > 0) {
			console.log("amount too small");
			return false;
		}
		SetIsSearchFieldValid(true);
		SetIsAmountValid(true);
		return true;
	};

	const unitChangeHandler = (event) => {
		setUnit(event);
	};

	const clearInput = () => {
		setAmount("");
	};

	const focusLost = () => {
		console.log("focus was lost");
	};

	return (
		<Card className="add-item-form">
			<form
				onSubmit={submitItemHandler}
				className={`${!isSearchFieldValid ? "invalid" : ""}`}
			>
				<SearchField
					onItemChanged={itemChangeHandler}
					onFocusLost={focusLost}
				/>
				<UnitBox className="form-units" onUnitSelected={unitChangeHandler} />
				<input
					className={`input-amount-field ${!isAmountValid ? "invalid" : ""}`}
					type="number"
					step="0.01"
					id="amount"
					placeholder="antal/mængde"
					onFocus={clearInput}
					value={amount}
					onChange={amountChangeHandler}
				></input>
				<Button type="submit" className="add-item-button">
					Tilføj Vare
				</Button>
				<br></br>
			</form>
		</Card>
	);

	async function fetchItemInfo(itemInfo) {
		const request = await fetch(
			`https://prisninjawebapi.azurewebsites.net/productinfo/${itemInfo}/`, //<-- er det denne url?
			{
				method: "GET",
				headers: {
					"Content-Type": "application/json",
				},
			}
		);
		const response = await request.json();
		console.log(response);
		return response;
	}

	async function fetchItems() {
		const request = await fetch(
			"https://prisninjawebapi.azurewebsites.net/names/",
			{
				method: "GET",
				headers: {
					"Content-Type": "application/json",
				},
			}
		);

		localStorage.setItem("itemNames", JSON.stringify(await request.json()));

		const response = await JSON.parse(localStorage.getItem("itemNames"));
		console.log(response);
	}
};

export default NewItemForm;
