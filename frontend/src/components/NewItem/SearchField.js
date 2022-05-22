import { TextField, Autocomplete } from "@mui/material";
import { v4 as uuid } from "uuid";
import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";

/**
 *  @classdesc
 * This is the component where the user write the name of the item to add. The main component is the AutoComplete component from MUI component library: https://mui.com/material-ui/react-autocomplete/
 *
 *
 * @category Home
 * @subcategory NewItem
 * @component
 * @hideconstructor
 */

const SearchField = (props) => {
	const itemsReceived = localStorage.hasOwnProperty("itemNames")
		? JSON.parse(localStorage.getItem("itemNames"))
		: [];

	const inputStyle = {
		paddingLeft: ".4rem",
	};

	const styles = (theme) => ({
		margin: "0",
		marginBottom: ".4rem",
		padding: "0",
		border: "0",
		borderBottom: "0",
		width: "102%",
		height: "2.5rem",

		[theme.breakpoints.down("576")]: {
			width: "122%",
		},
	});
	/**
	 * Sets the state of the itemName variable to the value of the input field.
	 * @const value
	 * @type {useState}
	 * @memberof SearchField
	 */
	const [value, setValue] = useState("");
	/**
	 * Controls the state of the AutoComplete dropdown menu. Only opens when open = true
	 * @const open
	 * @type {useState}
	 * @memberof SearchField
	 */
	const [open, setOpen] = useState(false);
	/**
	 * Sets the state of the items visible in the AutoComplete dropdown menu.
	 * @const searchValues
	 * @type {useState}
	 * @memberof SearchField
	 */
	const [searchValues, setSearchValues] = useState([itemsReceived]);

	/**
	 * Calls the onItemChanged event containing the current value of the input field every time the value changes.
	 * @function useEffect
	 * @memberof SearchField
	 */
	useEffect(() => {
		props.onItemChanged(value);
		// if (value.length > 1 && itemsReceived.includes(value))
		// 	handleFocusLoss(value);
	}, [value]);

	/**
	 * Calls onFocusLost event when the input field loses focus and the value in the input field can be found in the item names array
	 * received from the database.
	 *
	 * @param {string} newValue The name of the item written in the input field
	 * @returns {void} Nothing
	 */
	const handleFocusLoss = (newValue) => {
		setOpen(false);
		// console.log("handleFocusLoss was called with ", newValue);
		if (itemsReceived.includes(newValue)) {
			props.onFocusLost(newValue);
		}
	};

	/**
	 * @param {event} onInput - The event that is triggered when the input field is changed.
	 * @returns {void} Nothing
	 */
	const inputEventHandler = (event) => {
		const input = event.target.value;
		setValue(input);

		// this should prevent slow rendering issues with very little negative effect
		setSearchValues(
			itemsReceived.filter((item) => item.includes(input)).splice(0, 100)
		);
		if (input.length < 2) {
			setOpen(false);
		} else setOpen(true);
	};

	/**
	 * Ensures that the AutoComplete dropdown menu actually closes on focus loss.
	 *
	 * @param {event} onBlur - The event that is triggered when the input field loses focusLost
	 * @returns {void} Nothing
	 */
	const blurHandler = (event) => {
		setOpen(false);
		handleFocusLoss(event.target.value);
	};

	return (
		<Autocomplete
			open={open}
			options={searchValues}
			disablePortal
			clearOnEscape
			clearOnBlur
			freeSolo
			blurOnSelect
			disableClearable
			disableCloseOnSelect={false}
			onBlur={blurHandler}
			sx={styles}
			renderOption={(props, options) => {
				return (
					<li {...props} key={uuid()}>
						<span>{options}</span>
					</li>
				);
			}}
			value={value}
			onChange={(event, newValue) => {
				setValue(newValue);
				setOpen(false);
				handleFocusLoss(newValue);
			}}
			renderInput={(params) => (
				<TextField
					sx={{
						paddingLeft: ".2rem",
						paddingRight: ".2rem",
					}}
					{...params}
					placeholder="Tilføj Vare Her"
					variant="standard"
					// onKeyDown={(event) => {
					// 	if (value.length < 2) setOpen(false);
					// 	else setOpen(true);
					// }}
					onInput={inputEventHandler}
					inputProps={{
						...params.inputProps,
						style: inputStyle,
					}}
				/>
			)}
		/>
	);
};

SearchField.propTypes = {
	/**
	 * The input prop is only used when the component needs to be cleared.
	 */
	input: PropTypes.string,
	/**
	 * Fires an event when the user selects an item from the list.
	 */
	onItemChanged: PropTypes.func,
	/**
	 * Fires an event when the user clicks outside of the component or presses the enter or tabs key.
	 */
	onFocusLost: PropTypes.func,
};

export default SearchField;
