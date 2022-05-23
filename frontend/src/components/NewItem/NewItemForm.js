import "./NewItemForm.css";
import PropTypes from "prop-types";
import UnitBox from "./UnitBox";
import React, { useState, useEffect } from "react";
import { v4 as uuid } from "uuid";
import SearchField from "./SearchField";
import Card from "../UI/Atoms/Card/Card";
import Button from "../UI/Atoms/Button/Button";
import { FormControlLabel, Switch } from "@mui/material";

/**
 * @classdesc
 * This is the form where the user can add a new item to the list. This is comprised of two other components, specifically the UnitBox and SearchField.
 * The UnitBox is where the user can select the unit of the new item they're adding. The SearchField is where the user can search for an item to add to the list.
 *
 *
 * @category Home
 * @subcategory NewItem
 * @component
 * @hideconstructor
 *
 */

const NewItemForm = (props) => {
	/**
	 * The state of the name of the item the user is adding as received from the SearchField component.
	 * @const newItem
	 * @type {useState}
	 * @memberof NewItemForm
	 */
	const [newItem, setNewItem] = useState(null);

	/**
	 * The state of the amount of the item the user is adding.
	 * @const amount
	 * @type {useState}
	 * @memberof NewItemForm
	 */
	const [amount, setAmount] = useState("");

	/**
	 * The state of the unit of the item the user is adding as received from the UnitBox component. Default value is "kg"
	 * @const unit
	 * @type {useState}
	 * @memberof NewItemForm
	 */
	const [unit, setUnit] = useState("kg");

	/**
	 * This state controls which units are rendered by the UnitBox component.
	 * @const unitsAvailable
	 * @type {useState}
	 * @memberof NewItemForm
	 */
	const [unitsAvailable, setUnitsAvailable] = useState(["kg", "l", "stk"]);

	/**
	 * This state controls whether the item being added should be organic or not.
	 * @const organic
	 * @type {useState}
	 * @memberof NewItemForm
	 */
	const [organic, setOrganic] = useState(false);

	/**
	 * This state controls whether the "organic" switch is rendered or not.
	 * @const organicPossible
	 * @type {useState}
	 * @memberof NewItemForm
	 */
	const [organicPossible, setOrganicPossible] = useState(false);

	// changed validInput
	// const [isSearchFieldValid, SetIsSearchFieldValid] = useState(true);
	// const [isAmountValid, SetIsAmountValid] = useState(true);

	/**
	 * This is the id of the new item being added, it uses the uuid library to ensure a unique id
	 * @type {number}
	 */
	const id = uuid();

	/**
	 * Calls the onItemChanged event containing the current value of the input field every time the value changes.
	 * @function useEffect
	 * @memberof NewItemForm
	 */
	useEffect(() => {
		fetchItems();
	}, []);

	/**
	 * 
	 * @param {event} submitEvent 
	 * @returns {void} Nothing
	 */
	const submitItemHandler = (event) => {
		event.preventDefault();

		if (validInput(newItem, amount)) {
			props.onItemAdded(newItem, amount, unit, id, organic);
		} else return;

		setAmount("");
		setNewItem("");
		setUnit(`kg`);
		setOrganicPossible(false);
		setOrganic(false);
	};

	const switchStyling = {
		color: "white",
		"&:checked": {
			color: "green",
		},
		icon: "../../../public/images/økomærke.png",
	};

	/**
	 * When a user inputs an amount in the form, this updates the component state
	 *
	 * @param {event} event - The input event that triggered the function
	 * @returns {void}
	 */
	const amountChangeHandler = (event) => {
		const added = event.target.value;
		added > 0 ? setAmount(added) : setAmount("");
	};

	/**
	 * Helper function that checks whether the input format is correct
	 * @param {string} newItem - The name of the item
	 * @param {string} amount - The amount of the item
	 * @returns {boolean} Whether the input is valid or not
	 */
	const validInput = (newItem, amount) => {
		if (newItem === null || !newItem.length > 0) {
			// SetIsSearchFieldValid(false);
			console.log("No item was detected in the input field");
			return false;
		}
		if (amount === null || !amount.length > 0) {
			// SetIsAmountValid(false);
		}
		if (!amount > 0) {
			console.log("amount too small");
			return false;
		}
		// SetIsSearchFieldValid(true);
		// SetIsAmountValid(true);
		return true;
	};

	/**
	 * Clears the amount field on focus
	 * @returns {void}
	 */
	const clearInput = () => {
		setAmount("");
	};

	/**
	 * This handles when the organic switch is toggled.
	 *
	 * @param {event} onChange
	 *
	 */
	const organicHandler = (event) => {
		console.log(event.target.checked);
		setOrganic(event.target.checked);
	};

	/**
	 * When a user no longer has focus on the item name input field, this checks the database for the unit(s) this item normally has and
	 * whether there are organic variants and then rerenders the UnitBox component based on the info received
	 *
	 * @param {event} customEvent - From the SearchField component
	 * @returns {void} Nothing
	 */
	const focusLost = async (event) => {
		const item = await getUnitAndOrganic(event);
		if (!item) return;
		console.log("These units are available: ", item.units);
		setUnitsAvailable(item.units);
		setUnit(item.units[0]);

		if (item) {
			setOrganicPossible(item.organic);
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
					onItemChanged={(item) => setNewItem(item)}
					onFocusLost={focusLost}
					input={newItem}
				/>
				<div className="unit-organic-switch">
					<UnitBox
						className="form-units"
						onUnitSelected={(event) => setUnit(event)}
						unitChosen={unit}
						unitsAvailable={unitsAvailable}
					/>
					{organicPossible && (
						<FormControlLabel
							control={<Switch sx={switchStyling} onChange={organicHandler} />}
							label="Øko"
							sx={{
								color: "white",
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

	/**
	 * This function fetches the unit and organic status of the item written in SearchField from the database. If the items is not found, it returns false.
	 *
	 * @param {string} itemInfo The name of the item written in the SearchField
	 * @returns {object | boolean} The unit and organic status of the item
	 */

	async function fetchItemInfo(itemInfo) {
		let response;
		try {
			const request = await fetch(
				`https://prisninjawebapi.azurewebsites.net/productinfo/${itemInfo}/`,
				{
					method: "GET",
					headers: {
						"Content-Type": "application/json",
					},
				}
			);
			response = await request.json();
		} catch (error) {
			console.log("API call error : ", error);
			response = false;
		}
		return response;
	}

	/**
	 * This function checks the database for the unit this item normally has and whether there are organic variants and then rerenders based on the info received
	 *
	 * @returns {void} Nothing
	 */
	async function fetchItems() {
		let request = false;
		try {
			request = await fetch(
				"https://prisninjawebapi.azurewebsites.net/names/",
				{
					method: "GET",
					headers: {
						"Content-Type": "application/json",
					},
				}
			);
		} catch (error) {
			console.log("itemName API call error : ", error);
		}

		if (request)
			localStorage.setItem("itemNames", JSON.stringify(await request.json()));

		const response = await JSON.parse(localStorage.getItem("itemNames"));
		if (response.length > 15)
			console.log(`${response.length} item names were fetched`);
	}

	/**
	 * This function fetches the unit and organic status of the item written in SearchField from the database. If the items is not found, it returns null.
	 * @param {string} itemName The name of the item written in the SearchField
	 * @returns {object | null} The unit and organic status of the item or null
	 */
	async function getUnitAndOrganic(item) {
		const units = [];
		const info = await fetchItemInfo(item);
		console.log("Item info: ", info);
		if (!info) return;
		const organic = info.organic;
		const unitInfo = Object.keys(info).filter((i) => info[i] === true);

		// saves all units that are available for the item
		if (unitInfo.includes("measureG")) units.push("kg");
		if (unitInfo.includes("measureL")) units.push("l");
		if (unitInfo.includes("measureStk")) units.push("stk");

		return { units, organic };
	}
};

NewItemForm.propTypes = {
	/** Event that is fired when the user submits the form. Sends the name of the item, amount, unit, id and whether the item should be organic to the parent component.
	 */
	onItemAdded: PropTypes.func,
};

export default NewItemForm;
