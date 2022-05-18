import { FormControlLabel, Radio, RadioGroup } from "@mui/material";

const UnitBox = (props) => {
	const unitChangedHandler = (event) => {
		console.log(event.target.value);
		return props.onUnitSelected(event.target.value);
	};
	const kg = props.unitsAvailable.includes("kg");
	const liter = props.unitsAvailable.includes("l");
	const stk = props.unitsAvailable.includes("stk");
	console.log(
		"units sent to UnitBox : ",
		"kg:",
		kg,
		" - litre:",
		liter,
		" - stk :",
		stk
	);

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
			{kg && (
				<FormControlLabel
					value="kg"
					control={<Radio size="small" sx={radioStyles} />}
					label="Kg"
					sx={{
						color: "white",
					}}
				/>
			)}
			{liter && (
				<FormControlLabel
					value="l"
					control={<Radio size="small" sx={radioStyles} />}
					label="L"
					sx={{
						color: "white",
					}}
				/>
			)}
			{stk && (
				<FormControlLabel
					value="stk"
					control={<Radio size="small" sx={radioStyles} />}
					label="Stk"
					sx={{
						color: "white",
					}}
				/>
			)}
		</RadioGroup>
	);
};

export default UnitBox;
