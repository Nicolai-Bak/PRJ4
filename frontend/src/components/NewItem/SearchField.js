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
		console.log(`value changed to ${value}`);
		props.onItemChanged(value);
	}, [value]);

	return (
		<Autocomplete
			openOnFocus={false}
			autoComplete={true}
			autoHighlight
			autoSelect
			className="auto-complete"
			disablePortal
			label="Tilføj Vare Her"
			disableClearable
			freeSolo
			sx={{
				width: "50%",
				margin: "0",
				padding: "0",
				border: "0",
				borderBottom: "0",
			}}
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
			}}
			renderInput={(params) => (
				<TextField
					{...params}
					fullWidth
					label="Tilføj Vare Her"
					placeholder="Tilføj Vare Her"
					variant="standard"
					onChange={(event) => {
						setValue(event.target.value);
					}}
				/>
			)}
		/>
	);
};

export default SearchField;
