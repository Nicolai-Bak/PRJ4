import { TextField, Autocomplete } from "@mui/material";
import { v4 as uuid } from "uuid";
import React, { useState, useEffect } from "react";

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
	const [value, setValue] = useState("");
	const [open, setOpen] = useState(false);
	const [searchValues, setSearchValues] = useState([itemsReceived]);

	// const defaultProps = {
	// 	options: itemsReceived,
	// 	getOptionLabel: (option) => option,
	// };

	useEffect(() => {
		props.onItemChanged(value);
		// if (value.length > 1 && itemsReceived.includes(value))
		// 	handleFocusLoss(value);
	}, [value]);

	const handleFocusLoss = (newValue) => {
		setOpen(false);
		// console.log("handleFocusLoss was called with ", newValue);
		if (itemsReceived.includes(newValue)) {
			props.onFocusLost(newValue);
		}
	};

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
					placeholder="Tilf??j Vare Her"
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

export default SearchField;
