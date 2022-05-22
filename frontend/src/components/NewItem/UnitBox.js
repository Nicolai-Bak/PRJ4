import { FormControlLabel, Radio, RadioGroup } from "@mui/material";
import PropTypes from "prop-types";
/**
 * @classdesc
 * This component is where the user can select the unit of the new item they're adding.
 * The component is comprised of multiple components from the Material UI library. Primarily the RadioGroup (https://mui.com/material-ui/react-radio-button/)
 *
 *
 * @category Home
 * @subcategory NewItem
 * @component
 * @hideconstructor
 */
const UnitBox = (props) => {
	/**
	 *
	 * @param {event} event - onChange event that is triggered when the user changes the unit of the item they're adding.
	 * @returns {event} onUnitSelected custom event
	 */
	const unitChangedHandler = (event) => {
		return props.onUnitSelected(event.target.value);
	};
	/**
	 * Boolean value that checks whether the props array contains the string "kg"
	 */
	const kg = props.unitsAvailable.includes("kg");
	/**
	 * Boolean value that checks whether the props array contains the string "l"
	 */
	const liter = props.unitsAvailable.includes("l");

	/**
	 * Boolean value that checks whether the props array contains the string "stk"
	 */
	const stk = props.unitsAvailable.includes("stk");

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

UnitBox.propTypes = {
	/**
	 * The unit that is available is sent down from the parent. It only shows the units that are received from the database, using the productInfo request.
	 */
	unitsAvailable: PropTypes.array.isRequired,
	/**
	 * An event that fires when the user selects a unit. Tells the parent to update the unit chosen.
	 */
	onUnitSelected: PropTypes.func,
};

export default UnitBox;
