import "./NewItemForm.css";
import UnitBox from "./UnitBox";
import React, { useState } from "react";

const NewItemForm = (props) => {
	const [newItem, setNewItem] = useState("");
	const [amount, setAmount] = useState("");
	const [unit, setUnit] = useState("kg");

	const submitItemHandler = (event) => {
		event.preventDefault();
		console.log(`You just tried to add ${amount} ${unit} ${newItem}'s`);

		if (validInput(newItem, amount)) {
			props.onItemAdded(newItem, amount, unit);
		} else return;

		setNewItem("");
		setAmount("");
	};

	const itemChangeHandler = (event) => {
		setNewItem(event.target.value);
	};

	const amountChangeHandler = (event) => {
		const added = event.target.value;
		added < 0
			? console.log(`Item amount too small, was: ${added}`)
			: setAmount(added);
	};

	const validInput = (newItem, amount) => {
		if (!newItem.length > 0) {
			console.log("No item was detected in the input field");
			return false;
		}
		if (!amount > 0) {
			console.log("amount too small");
			return false;
		}

		return true;
	};

	const unitChangeHandler = (event) => {
		setUnit(event);
	};

	const clearInput = () => {
		setAmount("");
	};

	return (
		<form onSubmit={submitItemHandler} className="add-item-form">
			<div className="item-inputs">
				<input
					className="item-name"
					type="text"
					value={newItem}
					onChange={itemChangeHandler}
					placeholder="Tilføj varer her"
				></input>
			</div>
			<UnitBox onUnitSelected={unitChangeHandler}/>
			<input
				type="number"
				step="0.01"
				id="amount"
				placeholder="antal/mængde"
				onFocus={clearInput}
				value={amount}
				onChange={amountChangeHandler}
			></input>
			<button type="submit" className="add-item-button">
				Tilføj Vare
			</button>
			<br></br>
		</form>
	);
};

export default NewItemForm;
