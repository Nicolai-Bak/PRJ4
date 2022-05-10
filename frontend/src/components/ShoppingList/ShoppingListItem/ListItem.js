import React, { useState } from "react";
import "./ListItem.css";
import Button from "../../UI/Atoms/Button/Button";
import { IoTrashBin } from "react-icons/io5";

const ListItem = (props) => {
	const [displayAmount, setDisplayAmount] = useState(true);

	const amountChanged = (value) => {
		const validUnits = ["stk", "kg", "g", "l", "ml"];
		let numberOfUnits = 0;
		// Removes all values that are not letters and saves the remaining
		const unit = value
			.toString()
			.toLowerCase()
			.replace(/[^a-z]/g, "");
		// Removes everything except numbers, dots and commas
		// Needs to be improved since only one comma or dot should be allowed
		let newAmount = value
			.toString()
			.toLowerCase()
			.replace(/[^0-9.,]+$/, "");

		// testing to see how many valid units were found
		validUnits.forEach((x) => {
			if (x === unit) numberOfUnits++;
		});
		// if user writes no unit or an invalid unit
		if (numberOfUnits < 1) {
			props.newUnitOrAmount(props.id, newAmount, null);
			return;
		}
		// if user changes amount and unit
		if (numberOfUnits === 1) {
			props.newUnitOrAmount(props.id, newAmount, unit);
			return;
		}
		// edge case - user added multiple valid units
		if (numberOfUnits > 1) {
			alert("Mere end en enhed i indtastningsfeltet");
			return;
		}
		console.log("This should never be printed :) :| :(");
	};

	// Function that handles the rendering of the input field where the user can change amount and unit and 'amountChanged' with the new info
	const changeAmountAndUnit = (event) => {
		// This renders the input field where the user can change the amount and unit
		if (event.type === "click") {
			setDisplayAmount(!displayAmount);
			return;
		}

		// If there is something written in the amount field, we want to change the amount and unit if a unit has been input
		if (
			event.key === "Enter" ||
			event.type === "blur" ||
			event.key === "Escape"
		) {
			if (event.target.value.length < 1) {
				setDisplayAmount(true);
				return;
			}

			console.log("amountChanged called with : " + event.target.value);
			amountChanged(event.target.value);
			setDisplayAmount(!displayAmount);
		}
	};
	// This decides what is displayed when the user interacts with the span that shows unit and amount
	const amountAndUnitDisplay = () => {
		if (displayAmount) {
			return (
				<span className="unit-amount" onClick={changeAmountAndUnit}>
					{unitAmountHandler()}
				</span>
			);
		} else {
			return (
				<span className="unit-amount">
					<input
						autoFocus
						placeholder={props.amount + props.unit}
						className="unit-amount-input"
						type="text"
						onBlur={changeAmountAndUnit}
						onKeyDown={changeAmountAndUnit}
					/>
				</span>
			);
		}
	};

	return (
		<div className="list-item">
			<span className="list-item__left">{props.name}</span>
			<span className="list-item__right">
				<span className="item-buttons">
					<Button
						className="adjust-amount-button"
						onClick={() => props.onDecreaseAmount(props.id)}
					>
						-
					</Button>
					{amountAndUnitDisplay()}
					<Button
						className="adjust-amount-button"
						onClick={() => props.onIncreaseAmount(props.id)}
					>
						+
					</Button>
				</span>
				<IoTrashBin
					title="slet"
					className="remove-button"
					onClick={() => props.onRemoveItem(props.id, props.name)}
				/>
			</span>
		</div>
	);

	// This function attempts to prevent seeing values like 0.3kg and instead sees 300g to make it more readable
	function unitAmountHandler() {
		if (props.amount < 1 && props.unit.toString().toLowerCase() === "kg") {
			return props.amount * 1000 + "g";
		} else if (
			props.amount < 1 &&
			props.unit.toString().toLowerCase() === "l"
		) {
			return props.amount * 1000 + "ml";
		} else {
			return props.amount + props.unit;
		}
	}
};

export default ListItem;
