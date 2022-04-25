import { Button, TextField, Stack } from "@mui/material";
import { Autocomplete } from "@mui/material";
import { v4 as uuid } from "uuid";
import { useState, useEffect } from "react";

const SearchField = (props) => {
	const options = localStorage.hasOwnProperty("itemNames")
		? JSON.parse(localStorage.getItem("itemNames"))
		: [];

	const [value, setValue] = useState(null);

	const defaultProps = {
		options: options,
		getOptionLabel: (option) => option,
	};

	useEffect(() => {
		props.onItemChanged(value);
		const itemValue = setTimeout(() => {
			// console.log("NOW we call lost focus with " + value);
			if (value > 1) handleFocusLoss(value);
		}, 1000);

		return () => {
			// console.log("NOT calling lost focus yet");
			clearTimeout(itemValue); // <-- clean up function so itemValue is not called all the time
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
			sx={{
				width: "100%",
				margin: "0",
				padding: "0",
				border: "0",
				borderBottom: "0",
			}}
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
					{...params}
					fullWidth
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
				/>
			)}
		/>
	);
};

export default SearchField;
