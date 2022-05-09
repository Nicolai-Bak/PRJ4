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
	const [open, setOpen] = useState(false);
	const [searchValues, setSearchValues] = useState([]);

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
		setOpen(false);
		console.log("handleFocusLoss was called with " + event);
		props.onFocusLost(event);
	};

	const inputEventHandler = (event) => {
		setValue(event.target.value);
	};

	return (
		<Autocomplete
			open={open}
			openOnFocus={false}
			disablePortal
			disableClearable
			blurOnSelect
			freeSolo
			onBlur={handleFocusLoss}
			sx={styles}
			{...defaultProps}
			renderOption={(props, option) => {
				for (let i = 0; i < options.length; i++) {
					return (
						<li {...props} key={uuid()}>
							<span>{option}</span>
						</li>
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
					onKeyDown={(event) => {
						if (value.length < 2) setOpen(false);
						else setOpen(true);
					}}
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
