import {
	FormControl,
	FormControlLabel,
	Radio,
	RadioGroup,
	Switch,
} from "@mui/material";

import React, { forwardRef, useState } from "react";
import "./UnitBox.css";
import { styled } from "@mui/material/styles";

const UnitBox = (props) => {
	const unitChangedHandler = (event) => {
		console.log(event.target.value);
		return props.onUnitSelected(event.target.value);
	};

	const Responsive = styled("Radio")(({ theme }) => ({
		[theme.breakpoints.up("lg")]: {
			flexDirection: "row",
		},

		[theme.breakpoints.down("md")]: {
			flexDirection: "column",
			color: "purple",
		},
	}));

	// const classes = `unitBox__container ${props.className}`;
	const radioStyles = {
		color: "white",
		margin: "0",
		height: "1em",
		padding: "0",
		border: "0",
		borderBottom: "0",
		"&, &.Mui-checked": {
			color: "white",
		},
	};

	const options = JSON.parse(localStorage.getItem("itemNames"));
	// return (
	// 	<div className={classes}>
	// 		<input
	// 			type="radio"
	// 			name="unit"
	// 			id="kilogram"
	// 			value="kg"
	// 			defaultChecked
	// 			onChange={unitChangedHandler}
	// 		></input>
	// 		<label for="kilogram" tabIndex="1">
	// 			kg
	// 		</label>
	// 		<input
	// 			type="radio"
	// 			name="unit"
	// 			id="liter"
	// 			value="L"
	// 			onChange={unitChangedHandler}
	// 		></input>
	// 		<label for="liter" tabIndex="1">
	// 			liter
	// 		</label>
	// 		<input
	// 			type="radio"
	// 			name="unit"
	// 			id="piece"
	// 			value="stk"
	// 			onChange={unitChangedHandler}
	// 		></input>
	// 		<label for="piece" tabIndex="1">
	// 			stk
	// 		</label>
	// 	</div>
	// );

	return (
		<RadioGroup
			className="radio-buttons"
			onChange={unitChangedHandler}
			row
			value={props.unitChosen}
			sx={{
				marginLeft: ".7rem",
				padding: "0",
			}}
		>
			<FormControlLabel
				value="kg"
				control={<Radio size="small" sx={radioStyles} />}
				label="Kg"
				sx={{
					color: "white",
				}}
			/>
			<FormControlLabel
				value="l"
				control={<Radio size="small" sx={radioStyles} />}
				label="L"
				sx={{
					color: "white",
				}}
			/>
			<FormControlLabel
				value="stk"
				control={<Radio size="small" sx={radioStyles} />}
				label="Stk"
				sx={{
					color: "white",
				}}
			/>
		</RadioGroup>
	);
};

export default UnitBox;
