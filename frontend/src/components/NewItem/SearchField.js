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
	const [searchValues, setSearchValues] = useState([]);

	// const defaultProps = {
	// 	options: itemsReceived,
	// 	getOptionLabel: (option) => option,
	// };

	useEffect(() => {
		props.onItemChanged(value);
		if (value.length > 1 && itemsReceived.includes(value))
			handleFocusLoss(value);
	}, [value]);

	const handleFocusLoss = (event) => {
		console.log("handleFocusLoss was called with " + event);
		props.onFocusLost(event);
	};

	const inputEventHandler = (event) => {
		const input = event.target.value;
		setValue(input);

		setSearchValues(itemsReceived.filter((item) => item.includes(input)));
		if (input.length < 2) {
			setOpen(false);
		} else setOpen(true);
	};

	return (
		<Autocomplete
			open={open}
			openOnFocus={false}
			options={searchValues}
			disablePortal
			freeSolo
			disableClearable
			onBlur={handleFocusLoss}
			sx={styles}
			renderOption={(props, options) => {
				const number = 0;
				for (let i = 0; i < 5; i++) {
					return (
						<React.Fragment>
							<li {...props} key={uuid()}>
								<span>{options}</span>
							</li>
						</React.Fragment>
					);
				}
			}}
			value={value}
			onChange={(event, newValue) => {
				setValue(newValue);
				// console.log("newValue: ", newValue);
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

export default SearchField;
