import React, { useState } from "react";
import "./ListItem.css";
import Button from "../../UI/Atoms/Button/Button";
import { IoTrashBin } from "react-icons/io5";
import PropTypes from "prop-types";
import { listClasses, Tooltip } from "@mui/material";

/**
 * @classdesc
 * This is the component that represents a single item in the list. The user can interact with the item in multiple ways: delete the item by clicking the trash icon, change the amount and/or unit of the item by clicking the amount input, or increment/decrement by using the '+' and '-' buttons.
 *
 *
 * @category Home
 * @subcategory ShoppingList
 * @component
 * @hideconstructor
 *
 */

const ListItem = (props) => {
	const [displayAmount, setDisplayAmount] = useState(true);

	/**
	 * 
	 * This ensures that the the input from the in the amount field is a number and has a valid unit. It also handles which parameters the 'newUnitOrAmount' event is called with, assuming the input is valid.
	 * 
	 * @param {string} unitValue The unit of the item 
	 * @returns {void} Nothing
	 */
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

	/**
	 * Function that handles the rendering of the input field where the user can change amount and unit and 'amountChanged' with the new information. Also ensures that no change to the item is made if nothing was typed in the input field
	 * 
	 * @param {event} onClick Event that happens when the user  
	 * @returns {void} Nothing
	 */
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
	/**
	 * This decides what is displayed when the user interacts with the span that shows unit and amount. When the user clicks on the span, the input field is rendered. When the input field loses focus of the user presses 'Enter' or 'Escape', the input field is hidden and the span is updated with the new amount and unit if the user has changed the amount and unit.
	 *
	 * @returns {JSX} JSX that is rendered
	 */
	const amountAndUnitDisplay = () => {
		if (displayAmount) {
			return (
				<Tooltip
					enterDelay={600}
					leaveDelay={200}
					placement="top"
					title="Ændr på mængde eller enhed"
				>
					<span className="unit-amount" onClick={changeAmountAndUnit}>
						{unitAmountHandler()}
					</span>
				</Tooltip>
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

ListItem.propTypes = {
	/**
	 * The name of the item
	 */
	name: PropTypes.string.isRequired,
	/**
	 * The amount of the item
	 */
	amount: PropTypes.string.isRequired,
	/**
	 * The unit of the item
	 */
	unit: PropTypes.string.isRequired,
	/**
	 * The id of the item
	 */
	id: PropTypes.number.isRequired,
	/**
	 * Sets whether the item has to be organic or not
	 */
	organic: PropTypes.bool.isRequired,
	/**
	 * Event that fires when the '+' button is clicked
	 */
	onIncreaseAmount: PropTypes.func,
	/**
	 * Event that fires when the '-' button is clicked
	 */
	onDecreaseAmount: PropTypes.func,
	/**
	 * Event that fires when the garbage can icon is clicked
	 */
	onRemoveItem: PropTypes.func,
	/**
	 * Event that fires when the user changes the amount and unit through the input field
	 */
	newUnitOrAmount: PropTypes.func,
};

export default ListItem;
