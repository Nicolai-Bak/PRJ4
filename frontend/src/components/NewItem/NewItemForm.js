import "./NewItemForm.css";
import UnitBox from "./UnitBox";
import React, { useState, useEffect } from "react";
import { v4 as uuid } from "uuid";
import SearchField from "./SearchField";
import Card from "../UI/Atoms/Card/Card";
import Button from "../UI/Atoms/Button/Button";
import { FormControlLabel, Switch } from "@mui/material";

const NewItemForm = (props) => {
	const [newItem, setNewItem] = useState(null);
	const [amount, setAmount] = useState("");
	const [unit, setUnit] = useState("kg");
	const [organic, setOrganic] = useState(false);
	const [isChecked, setIsChecked] = useState(false);
	const [organicPossible, setOrganicPossible] = useState(false);

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
			props.onItemAdded(newItem, amount, unit, id, organic);
		} else return;

		// setNewItem(""); <-- doesn't reset the autocomplete field after submit
		setAmount("");
	};

	const switchStyling = {
		color: "white",
		"&:checked": {
			color: "green",
		},
		icon: "../../../public/images/økomærke.png",
	};

	const itemChangeHandler = (itemReceived) => {
		SetIsSearchFieldValid(true);
		setNewItem(itemReceived);
	};

	const amountChangeHandler = (event) => {
		const added = event.target.value;
		added < 0 ? SetIsAmountValid(false) : setAmount(added);
		SetIsAmountValid(true); // ?? Det her bliver jo altid kaldt?
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

	const organicHandler = (event) => {
		console.log(event.target.checked);
		setOrganic(event.target.checked);
	};

	const focusLost = async (event) => {
		console.log("focus was lost and the current value is " + event);
		const unit = await getUnitAndOrganic(event); // IKKE FÆRDIG!!

		console.log(unit);
		setUnit(unit.unit);

		if (unit) {
			setOrganicPossible(!unit.organic);
		}
	};
	return (
		<Card className="add-item-container">
			<form
				onSubmit={submitItemHandler}
				// className={`${!isSearchFieldValid ? "invalid" : ""}`}
				className={"add-item-form"}
			>
				<SearchField
					onItemChanged={itemChangeHandler}
					onFocusLost={focusLost}
				/>
				<div className="unit-organic-switch">
					<UnitBox
						className="form-units"
						onUnitSelected={unitChangeHandler}
						unitChosen={unit}
					/>
					{organicPossible && (
						<FormControlLabel
							control={<Switch sx={switchStyling} onChange={organicHandler} />}
							label="Øko"
							sx={{
								color: "white",
								icon: "../../../public/images/økomærke.png",
							}}
						/>
					)}
				</div>
				<input
					tabIndex="0"
					// className={`input-amount-field ${!isAmountValid ? "invalid" : ""}`}
					className="amount-input"
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
			</form>
		</Card>
	);

	async function fetchItemInfo(itemInfo) {
		const request = await fetch(
			`https://prisninjawebapi.azurewebsites.net/productinfo/${itemInfo}/`,
			{
				method: "GET",
				headers: {
					"Content-Type": "application/json",
				},
			}
		);
		const response = await request.json();
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

	async function getUnitAndOrganic(item) {
		let unit = null;
		const info = await fetchItemInfo(item);
		const organic = info.organic;
		const unitInfo = Object.keys(info).filter((i) => info[i] === true);

		if (unitInfo.includes("measureG")) unit = "kg";
		else if (unitInfo.includes("measureL")) unit = "l";
		else if (unitInfo.includes("measureStk")) unit = "stk";

		return { unit, organic };
	}
};

export default NewItemForm;
