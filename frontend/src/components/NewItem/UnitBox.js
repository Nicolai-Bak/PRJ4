import React from "react";
import { forwardRef} from "react";
import "./UnitBox.css";

const UnitBox = forwardRef((props, ref) => {
	const unitChangedHandler = (event) => {
		console.log(event.target.value);
		return props.onUnitSelected(event.target.value);
	};

	return (
		<div className="unitBox__container">
			<input
				type="radio"
				name="unit"
				id="kilogram"
				value="kg"
				defaultChecked
				onChange={unitChangedHandler}
			></input>
			<label for="kilogram" tabIndex="0">
				kg
			</label>
			<input
				type="radio"
				name="unit"
				id="liter"
				value="L"
				onChange={unitChangedHandler}
			></input>
			<label for="liter" tabIndex="0">
				liter
			</label>
			<input
				type="radio"
				name="unit"
				id="piece"
				value="stk"
				onChange={unitChangedHandler}
			></input>
			<label for="piece" tabIndex="0">
				stk
			</label>
		</div>
	);
});

export default UnitBox;
