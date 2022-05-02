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

	const defaultProps = {
		options: options,
		getOptionLabel: (option) => option,
	};

	useEffect(() => {
		props.onItemChanged(value);
		if (value.length > 1 && options.includes(value)) handleFocusLoss(value);
	}, [value]);

	const handleFocusLoss = (event) => {
		if (event.type) {
			event = event.target.value;
		}
		console.log("handleFocusLoss was called with " + event);
		props.onFocusLost(event);
	};

	return (
		<Autocomplete
			openOnFocus={false}
			disablePortal
			disableClearable
			freeSolo
			onBlur={handleFocusLoss}
			sx={styles}
			{...defaultProps}
			renderOption={(props, option) => {
				if (value === null || value.toString().length < 2) return;
				return (
					<li {...props} key={uuid()}>
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
					placeholder="Tilføj Vare Her"
					variant="standard"
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
