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

	const handleFocusLoss = () => {
		props.onFocusLost(value);
	};

	return (
		<Autocomplete
			openOnFocus={false}
			autoComplete={true}
			autoHighlight
			autoSelect
			disablePortal
			disableClearable
			freeSolo
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
					focusOff={() => {
						handleFocusLoss();
					}}
					// label="Tilføj Vare Her" //<-- skal IKKE slettes! tror jeg :P
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
