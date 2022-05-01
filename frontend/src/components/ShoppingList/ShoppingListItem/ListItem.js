import React, { useState } from "react";
import "./ListItem.css";
import Button from "../../UI/Atoms/Button/Button";
import { alertTitleClasses } from "@mui/material";

const ListItem = (props) => {
	const [displayAmount, setDisplayAmount] = useState(true);

	const unitAmountHandler = () => {
		if (props.amount < 1 && props.unit === "kg") {
			return props.amount * 1000 + "g";
		} else if (props.amount < 1 && props.unit === "l") {
			return props.amount * 1000 + "ml";
		} else {
			return props.amount + props.unit;
		}
	};

	const amountChanged = (value) => {
		const validUnits = ["stk", "kg", "g", "l", "ml"];
		// Gemmer alle værdier i 'value' som ikke er tal
		const unit = value.toString().replace(/[0-9]/g, "");
		// fjerner alle bogstaver - vi har ikke lyst til at fjerne kommaer/punktummer
		const newAmount = value.toString().toLowerCase().replace(/[a-z]/g, "");
		const addedUnit = [];
		validUnits.forEach((x) => {
			if (x === unit) {
				addedUnit.push(x);
			}
		});
		if (addedUnit.length > 1) {
			alert("Mere end en enhed i indtastningsfeltet");
			return;
		}
		if (addedUnit.length < 1) {
			props.newUnitOrAmount(props.id, newAmount, null);
			return;
		}

		if (addedUnit.length === 1) {
			props.newUnitOrAmount(props.id, newAmount, unit);
			return;
		}
		console.log("This should never be printed :) :| :(");
	};

	const changeAmountAndUnit = (event) => {
		if (event.type !== "keydown") {
			setDisplayAmount(!displayAmount);
			console.log(event.type);
		}
		console.log(event);

		if (event.key === "Enter") {
			console.log("amountChanged called with : " + event.target.value);
			amountChanged(event.target.value);
			setDisplayAmount(!displayAmount);
		}
	};

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
				<span className="unit-amount">{amountAndUnitDisplay()}</span>
				<span className="item-buttons">
					<Button
						className="adjust-amount-button"
						onClick={() => props.onDecreaseAmount(props.id)}
					>
						-
					</Button>
					<Button
						className="adjust-amount-button"
						onClick={() => props.onIncreaseAmount(props.id)}
					>
						+
					</Button>
				</span>
				<Button
					className="remove-button"
					onClick={() => props.onRemoveItem(props.id, props.name)}
				>
					Slet
				</Button>
			</span>
		</div>
	);
};

export default ListItem;
