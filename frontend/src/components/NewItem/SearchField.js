import { TextField, Autocomplete } from "@mui/material";
import { v4 as uuid } from "uuid";
import { useState, useEffect } from "react";

const SearchField = (props) => {
	const options = localStorage.hasOwnProperty("itemNames")
		? JSON.parse(localStorage.getItem("itemNames"))
		: [];

	const inputStyle = {
		paddingLeft: ".4rem",
	};

	const autoStyle = {
		margin: "0",
		marginBottom: ".4rem",
		padding: "0",
		border: "0",
		borderBottom: "0",
		width: "102%",
		height: "2.5rem",
	};

	const [value, setValue] = useState("");

	const defaultProps = {
		options: options,
		getOptionLabel: (option) => option,
	};

	useEffect(() => {
		props.onItemChanged(value);
		const itemValue = setTimeout(() => {
			// console.log("NOW we call lost focus with " + value);
			if (value.length > 1) handleFocusLoss(value);
		}, 1000);

		return () => {
			clearTimeout(itemValue); // <-- clean up function so itemValue isn't called if the timer doesn't run out
		};
	}, [value]);

	const handleFocusLoss = (event) => {
		console.log("handleFocusLoss was called with " + event);
		props.onFocusLost(event);
	};

	return (
		<Autocomplete
			openOnFocus={false}
			autoSelect={true}
			disablePortal
			disableClearable
			freeSolo
			// onBlur={(event) => {
			// 	handleFocusLoss("onBlur called: " + event.target.value);
			// }}
			sx={autoStyle}
			{...defaultProps}
			renderOption={(props, option) => {
				if (value === null || value.toString().length < 2) return;
				return (
					<li
						{...props}
						key={uuid()}
						// onClick={() => {
						// 	setValue(option);
						// 	console.log(`value changed to ${option}`);
						// 	handleFocusLoss(option);
						// }}
						// onKeyDown={(event) => {
						// 	if (event.key === "Enter" || event.key === "Tab") {
						// 		setValue(option);
						// 		handleFocusLoss(option);
						// 	}
						// }}
					>
						<span>{option}</span>
					</li>
				);
			}}
			value={value}
			onChange={(event, newValue) => {
				setValue(newValue);
				console.log("newValue: ", newValue);
			}}
			renderInput={(params) => (
				<TextField
					sx={{
						paddingLeft: ".2rem",
						paddingRight: ".2rem",
					}}
					{...params}
					// label="Tilføj Vare Her" //<-- skal IKKE slettes! tror jeg :P
					placeholder="Tilføj Vare Her"
					variant="standard"
					// onKeyDown={(event) => {
					// 	if (event.key === "Enter" || event.key === "Tab") {
					// 		handleFocusLoss();
					// 	}
					// }}
					onChange={(event) => {
						setValue(event.target.value);
					}}
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
