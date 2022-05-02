import { TextField, Autocomplete } from "@mui/material";
import { v4 as uuid } from "uuid";
import { useState, useEffect } from "react";
import { createTheme } from "@mui/system";
import { red } from "@mui/material/colors";

const SearchField = (props) => {
	const options = localStorage.hasOwnProperty("itemNames")
		? JSON.parse(localStorage.getItem("itemNames"))
		: [];

	const inputStyle = {
		paddingLeft: ".4rem",
	};

	const theme = createTheme({
		breakpoints: {
			values: {
				mobile: 0,
				tablet: 640,
				laptop: 1024,
				desktop: 1200,
			},
		},
	});

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
		console.log("handleFocusLoss was called with " + event);
		props.onFocusLost(event);
	};

	return (
		<Autocomplete
			openOnFocus={false}
			// autoSelect={true}
			disablePortal
			disableClearable
			freeSolo
			sx={styles}
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
