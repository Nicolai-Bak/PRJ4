import React from "react";
import "./UnitBox.css";

const UnitBox = (props) => {
	const unitChangedHandler = (event) => {
		console.log(event.target.value);
		return props.onUnitSelected(event.target.value);
	};

	const amountChangedHandler = (event) => {
		console.log(event.target.value);
		return props.onAmountChanged(event.target.value);
	};

	return (
		<React.Fragment>
			<form className="unitBox__container" onChange={unitChangedHandler}>
				<input type="radio" name="unit" id="kilogram" value="kg"></input>
				<label for="kilogram" tabIndex="0">
					kg
				</label>
				<input type="radio" name="unit" id="liter" value="L"></input>
				<label for="liter" tabIndex="0">
					liter
				</label>
				<input type="radio" name="unit" id="piece" value="piece"></input>
				<label for="piece" tabIndex="0">
					stk
				</label>
			</form>
			<input
				type="number"
				step="0.01"
				id="amount"
				placeholder="antal/mængde"
				onChange={amountChangedHandler}
			></input>
		</React.Fragment>
	);
};

export default UnitBox;
